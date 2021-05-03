namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Linq;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class LabelService : ILabelService
    {
        private const string SmallLabelPrinter = "ProdLbl1";

        private const string SmallClearLabelPrinter = "KlimaxClear";

        private const string ProductLabelTemplate = "c:\\lbl\\prodlbl.btw";

        private const string ProductLabelClearTemplate = "c:\\lbl\\prodLblClearDouble.btw";

        private const string MacLabelTemplate = "c:\\lbl\\prodlblctr.btw";

        private const string MacLabelClearTemplate = "c:\\lbl\\macAddressClearDouble.btw";

        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly ILabelPack labelPack;

        private readonly IRepository<ProductData, int> productDataRepository;

        private readonly IRepository<SerialNumber, int> serialNumberRepository;

        private readonly ISernosPack sernosPack;

        private readonly IRepository<LabelType, string> labelTypeRepository;

        private readonly ISalesArticleService salesArticleService;

        private string message;

        public LabelService(
            IBartenderLabelPack bartenderLabelPack,
            ILabelPack labelPack,
            IRepository<ProductData, int> productDataRepository,
            IRepository<SerialNumber, int> serialNumberRepository,
            ISernosPack sernosPack,
            IRepository<LabelType, string> labelTypeRepository,
            ISalesArticleService salesArticleService)
        {
            this.labelPack = labelPack;
            this.bartenderLabelPack = bartenderLabelPack;
            this.productDataRepository = productDataRepository;
            this.serialNumberRepository = serialNumberRepository;
            this.sernosPack = sernosPack;
            this.labelTypeRepository = labelTypeRepository;
            this.salesArticleService = salesArticleService;
        }

        private enum SmallLabelType
        {
            Normal,
            Clear
        }

        public void PrintMACLabel(int serialNumber)
        {
            this.PrintMacAddressLabel(
                serialNumber,
                this.GetMacAddress(serialNumber),
                this.GetSmallLabelType(serialNumber));
        }

        public void PrintLabel(string name, string printer, int qty, string template, string data)
        {
            this.bartenderLabelPack.PrintLabels(
                name,
                printer,
                qty,
                template,
                data,
                ref this.message);
        }

        public void PrintAllLabels(int serialNumber, string articleNumber)
        {
            var labelData = this.labelPack.GetLabelData("BOX", serialNumber, articleNumber);
            var productLabelData = this.labelPack.GetLabelData("PRODUCT", serialNumber, articleNumber);
            var smallLabelType = this.GetSmallLabelType(serialNumber, articleNumber);

            string macAddress = null;
            try
            {
                macAddress = this.GetMacAddress(serialNumber);
            }
            catch (DomainException)
            {
                // Still print the other labels if the product does not have a MAC address
            }

            if (!string.IsNullOrEmpty(macAddress))
            {
                this.PrintMacAddressLabel(serialNumber, this.GetMacAddress(serialNumber), smallLabelType);
            }

            this.PrintBoxLabel(serialNumber, labelData);
            this.PrintProductLabel(serialNumber, productLabelData, smallLabelType);
        }

        public LabelReprint CreateLabelReprint(
            int requestedByUserNumber,
            string reason,
            string partNumber,
            int? serialNumber,
            int? documentNumber,
            string labelTypeCode,
            int numberOfProducts,
            string reprintType,
            string newPartNumber)
        {
            if (string.IsNullOrEmpty(partNumber))
            {
                throw new LabelReprintInvalidException("No part number specified for label reprint");
            }

            partNumber = partNumber.ToUpper();
            newPartNumber = newPartNumber?.ToUpper();

            this.sernosPack.GetSerialNumberBoxes(partNumber, out var serialNumberQty, out var boxesQty);
            if (serialNumberQty > 1 && serialNumber % 2 == 0)
            {
                throw new LabelReprintInvalidException("You must use the odd number serial number of a pair");
            }

            if (this.sernosPack.SerialNumbersRequired(partNumber))
            {
                if (!serialNumber.HasValue)
                {
                    throw new LabelReprintInvalidException("You must specify a serial number for serial numbered products");
                }
            }

            if (serialNumber.HasValue)
            {
                if (!this.sernosPack.SerialNumberExists(serialNumber.Value, partNumber))
                {
                    throw new InvalidSerialNumberException($"No serial number {serialNumber.Value} exists for part {partNumber}");
                }
            }

            numberOfProducts = Math.Max(numberOfProducts, 1);

            if (reprintType == "REISSUE" || reprintType == "REBUILD")
            {
                if (!serialNumber.HasValue)
                {
                    throw new LabelReprintInvalidException("You must specify a serial number for Reissues or Rebuilds");
                }

                var productGroup = this.sernosPack.GetProductGroup(partNumber);
                if (string.IsNullOrEmpty(productGroup))
                {
                    throw new ProductGroupNotFoundException($"Could not find product group for {partNumber}");
                }

                switch (reprintType)
                {
                    case "REISSUE":
                        this.sernosPack.ReIssueSernos(partNumber, newPartNumber, serialNumber.Value);
                        break;
                    case "REBUILD":
                        this.RebuildSerialNumber(newPartNumber ?? partNumber, productGroup, serialNumber.Value, requestedByUserNumber);
                        break;
                }
            }
            else
            {
                newPartNumber = null;
            }

            this.PrintTheLabels(
                labelTypeCode,
                newPartNumber ?? partNumber,
                serialNumber,
                numberOfProducts,
                serialNumberQty,
                boxesQty);

            var labelReprint = new LabelReprint
                                   {
                                       LabelTypeCode = labelTypeCode,
                                       SerialNumber = serialNumber,
                                       DateIssued = DateTime.UtcNow,
                                       WorksOrderNumber = documentNumber,
                                       DocumentType = documentNumber.HasValue ? "WO" : null,
                                       NewPartNumber = newPartNumber,
                                       NumberOfProducts = numberOfProducts,
                                       PartNumber = partNumber,
                                       Reason = reason,
                                       ReprintType = reprintType,
                                       RequestedBy = requestedByUserNumber
                                   };
            return labelReprint;
        }

        private void PrintTheLabels(
            string labelTypeCode,
            string partNumber,
            int? serialNumber,
            int numberOfProducts,
            int numberOfSerialNumbers,
            int numberOfBoxes)
        {
            var labelData = this.labelPack.GetLabelData(labelTypeCode, serialNumber, partNumber);
            var labelType = this.labelTypeRepository.FindById(labelTypeCode);

            var printerName = labelType.DefaultPrinter;
            var templateName = labelType.Filename;

            if (labelTypeCode == "PRODUCT" && this.GetSmallLabelType(serialNumber, partNumber) == SmallLabelType.Clear)
            {
                printerName = SmallClearLabelPrinter;
                templateName = ProductLabelClearTemplate;
            }

            this.bartenderLabelPack.PrintLabels(
                $"LabelReprint{serialNumber}",
                printerName,
                numberOfProducts,
                templateName,
                labelData,
                ref this.message);

            if (numberOfBoxes == 2 || (labelTypeCode == "PRODUCT" && numberOfSerialNumbers == 2))
            {
                if (numberOfSerialNumbers == 2)
                {
                    labelData = this.labelPack.GetLabelData(labelTypeCode, serialNumber + 1, partNumber);
                }

                this.bartenderLabelPack.PrintLabels(
                    $"LabelReprint{serialNumber}B",
                    printerName,
                    numberOfProducts,
                    templateName,
                    labelData,
                    ref this.message);
            }
        }

        private void RebuildSerialNumber(string partNumber, string productGroup, int serialNumber, int requestedBy)
        {
            var newSerialNumber = new SerialNumber(productGroup, "REBUILD", partNumber)
                                      {
                                          SernosNumber = serialNumber, CreatedBy = requestedBy
                                      };
            this.serialNumberRepository.Add(newSerialNumber);
        }

        private void PrintProductLabel(
            int serialNumber,
            string labelData,
            SmallLabelType labelType = SmallLabelType.Normal)
        {
            if (labelType == SmallLabelType.Clear)
            {
                this.bartenderLabelPack.PrintLabels(
                    $"ProductReprint{serialNumber}",
                    SmallClearLabelPrinter,
                    3,
                    ProductLabelClearTemplate,
                    labelData,
                    ref this.message);
            }
            else
            {
                this.bartenderLabelPack.PrintLabels(
                    $"ProductReprint{serialNumber}",
                    SmallLabelPrinter,
                    3,
                    ProductLabelTemplate,
                    labelData,
                    ref this.message);
            }
        }

        private void PrintBoxLabel(int serialNumber, string labelData)
        {
            this.bartenderLabelPack.PrintLabels(
                $"BoxReprint{serialNumber}",
                "ProdLbl2",
                1,
                "c:\\lbl\\boxlbl_08ean.btw",
                labelData,
                ref this.message);
        }

        private string GetMacAddress(int serialNumber)
        {
            var productData = this.productDataRepository.FindById(serialNumber);
            if (productData == null)
            {
                throw new DomainException($"Could not find product data for serial number {serialNumber}");
            }

            return productData.MACAddress;
        }

        private void PrintMacAddressLabel(
            int serialNumber,
            string macAddress,
            SmallLabelType labelType = SmallLabelType.Normal)
        {
            if (labelType == SmallLabelType.Clear)
            {
                this.bartenderLabelPack.PrintLabels(
                    $"MACReprintm{serialNumber}",
                    SmallClearLabelPrinter,
                    2,
                    MacLabelClearTemplate,
                    macAddress,
                    ref this.message);
            }
            else
            {
                this.bartenderLabelPack.PrintLabels(
                    $"MACReprintm{serialNumber}",
                    SmallLabelPrinter,
                    2,
                    MacLabelTemplate,
                    macAddress,
                    ref this.message);
            }
        }

        private SmallLabelType GetSmallLabelType(int? serialNumber, string articleNumber = null)
        {
            if (string.IsNullOrEmpty(articleNumber))
            {
                var serialNumberDetails = this.serialNumberRepository.FilterBy(a => a.SernosNumber == serialNumber);
                articleNumber = serialNumberDetails.LastOrDefault()?.ArticleNumber;
            }

            var labelType = this.salesArticleService.GetSmallLabelType(articleNumber);
            return labelType == "CLEAR" ? SmallLabelType.Clear : SmallLabelType.Normal;
        }
    }
}
