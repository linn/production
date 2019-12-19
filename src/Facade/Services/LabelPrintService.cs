namespace Linn.Production.Facade.Services

{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Extensions;

    public class LabelPrintService : ILabelPrintService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly ILabelPack labelPack;

        private readonly IRepository<ProductData, int> productDataRepository;

        private readonly IRepository<SerialNumber, int> serialNumberRepository;

        private readonly ISernosPack sernosPack;

        private readonly IRepository<LabelType, string> labelTypeRepository;

        private string message;

        public LabelPrintService(
            IBartenderLabelPack bartenderLabelPack,
            ILabelPack labelPack,
            IRepository<ProductData, int> productDataRepository,
            IRepository<LabelType, string> labelTypeRepository)
        {
            this.labelPack = labelPack;
            this.bartenderLabelPack = bartenderLabelPack;
            this.productDataRepository = productDataRepository;
            this.labelTypeRepository = labelTypeRepository;
        }

        public IResult<IEnumerable<IdAndName>> GetPrinters()
        {
            var printerList = new List<IdAndName>();

            var allPrinterValues = Enum.GetValues(typeof(LabelPrinters.Printers));

            for (int i = 0; i < allPrinterValues.Length; i++)
            {
                var enumValue = (LabelPrinters.Printers)i;
                printerList.Add(new IdAndName(i, enumValue.GetDisplayName()));
            }

            return new SuccessResult<IEnumerable<IdAndName>>(printerList);
        }

        public IResult<IEnumerable<IdAndName>> GetLabelTypes()
        {
            var labelList = new List<IdAndName>();

            var allLabelValues = Enum.GetValues(typeof(GeneralPurposeLabelTypes.Labels));

            for (int i = 0; i < allLabelValues.Length; i++)
            {
                var enumValue = (GeneralPurposeLabelTypes.Labels)i;
                labelList.Add(new IdAndName(i, enumValue.GetDisplayName()));
            }

            return new SuccessResult<IEnumerable<IdAndName>>(labelList);
        }

        public void PrintLabel(int serialNumber, string articleNumber)
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

        public void PrintLabel(int serialNumber)
        {
            throw new NotImplementedException();
        }

        public LabelPrint CreateLabelPrint(int requestedByUserNumber, string reason, string partNumber, int? serialNumber, int? documentNumber, string labelTypeCode, int numberOfProducts, string reprintType, string newPartNumber)
        {
            throw new NotImplementedException();
        }
    }
}