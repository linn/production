namespace Linn.Production.Domain.LinnApps
{
    using System;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class LabelService : ILabelService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly ILabelPack labelPack;

        private readonly IRepository<ProductData, int> productDataRepository;

        private readonly IRepository<SerialNumber, int> serialNumberRepository;

        private readonly ISernosPack sernosPack;

        private readonly IRepository<LabelType, string> labelTypeRepository;

        private string message;

        public LabelService(
            IBartenderLabelPack bartenderLabelPack,
            ILabelPack labelPack,
            IRepository<ProductData, int> productDataRepository,
            IRepository<SerialNumber, int> serialNumberRepository,
            ISernosPack sernosPack,
            IRepository<LabelType, string> labelTypeRepository)
        {
            this.labelPack = labelPack;
            this.bartenderLabelPack = bartenderLabelPack;
            this.productDataRepository = productDataRepository;
            this.serialNumberRepository = serialNumberRepository;
            this.sernosPack = sernosPack;
            this.labelTypeRepository = labelTypeRepository;
        }

        public void PrintMACLabel(int serialNumber)
        {
            this.PrintMACAddressLabel(serialNumber, this.GetMACAddress(serialNumber));
        }

        public void PrintAllLabels(int serialNumber, string articleNumber)
        {
            var labelData = this.labelPack.GetLabelData("BOX", serialNumber, articleNumber);
            string macAddress = null;
            try
            {
                macAddress = this.GetMACAddress(serialNumber);
            }
            catch (DomainException)
            {
                // Still print the other labels if the product does not have a MAC address
            }

            if (!string.IsNullOrEmpty(macAddress))
            {
                this.PrintMACAddressLabel(serialNumber, this.GetMACAddress(serialNumber));
            }

            this.PrintBoxLabel(serialNumber, labelData);
            this.PrintProductLabel(serialNumber, labelData);
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

            this.bartenderLabelPack.PrintLabels(
                $"LabelReprint{serialNumber}",
                labelType.DefaultPrinter,
                numberOfProducts,
                labelType.Filename,
                labelData,
                ref this.message);

            if (numberOfBoxes == 2)
            {
                if (numberOfSerialNumbers == 2)
                {
                    labelData = this.labelPack.GetLabelData(labelTypeCode, serialNumber + 1, partNumber);
                }

                this.bartenderLabelPack.PrintLabels(
                    $"LabelReprint{serialNumber}B",
                    labelType.DefaultPrinter,
                    numberOfProducts,
                    labelType.Filename,
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

        private void PrintProductLabel(int serialNumber, string labelData)
        {
            this.bartenderLabelPack.PrintLabels(
                $"ProductReprint{serialNumber}",
                "ProdLbl1",
                3,
                "c:\\lbl\\prodlbl.btw",
                labelData,
                ref this.message);
        }

        private void PrintBoxLabel(int serialNumber, string labelData)
        {
            this.bartenderLabelPack.PrintLabels(
                $"BoxReprint{serialNumber}",
                "ProdLbl2",
                1,
                "c:\\lbl\\boxlbl_ean.btw",
                labelData,
                ref this.message);
        }

        private string GetMACAddress(int serialNumber)
        {
            var productData = this.productDataRepository.FindById(serialNumber);
            if (productData == null)
            {
                throw new DomainException($"Could not find product data for serial number {serialNumber}");
            }

            return productData.MACAddress;
        }

        private void PrintMACAddressLabel(int serialNumber, string macAddress)
        {
            this.bartenderLabelPack.PrintLabels(
                $"MACReprintm{serialNumber}",
                "ProdLbl1",
                2,
                "c:\\lbl\\prodlblctr.btw",
                macAddress,
                ref this.message);
        }
    }
}