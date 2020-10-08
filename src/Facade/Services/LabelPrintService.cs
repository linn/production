namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources;

    public class LabelPrintService : ILabelPrintService
    {
        private readonly ILabelPrintingService labelPrintingService;

        public LabelPrintService(
            ILabelPrintingService labelPrintingService)
        {
            this.labelPrintingService = labelPrintingService;
        }

        public IResult<IEnumerable<IdAndName>> GetPrinters()
        {
            var printerList = new List<IdAndName>();

            var allPrinterValues = Enum.GetValues(typeof(LabelPrinters.Printers));

            foreach (int i in allPrinterValues)
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

            foreach (int i in allLabelValues)
            {
                var enumValue = (GeneralPurposeLabelTypes.Labels)i;
                labelList.Add(new IdAndName(i, enumValue.GetDisplayName()));
            }

            return new SuccessResult<IEnumerable<IdAndName>>(labelList);
        }

        public IResult<LabelPrintResponse> PrintLabel(LabelPrintResource resource)
        {
            var printDetails = new LabelPrint
                                   {
                                       LabelType = resource.LabelType,
                                       LinesForPrinting = new LabelPrintContents
                                                              {
                                                                  SupplierId = resource.LinesForPrinting.SupplierId,
                                                                  Addressee = resource.LinesForPrinting.Addressee,
                                                                  Addressee2 = resource.LinesForPrinting.Addressee2,
                                                                  AddressId = resource.LinesForPrinting.AddressId,
                                                                  Line1 = resource.LinesForPrinting.Line1,
                                                                  Line2 = resource.LinesForPrinting.Line2,
                                                                  Line3 = resource.LinesForPrinting.Line3,
                                                                  Line4 = resource.LinesForPrinting.Line4,
                                                                  Line5 = resource.LinesForPrinting.Line5,
                                                                  Line6 = resource.LinesForPrinting.Line6,
                                                                  Line7 = resource.LinesForPrinting.Line7,
                                                                  PostalCode = resource.LinesForPrinting.PostalCode,
                                                                  Country = resource.LinesForPrinting.Country,
                                                                  PoNumber = resource.LinesForPrinting.PoNumber,
                                                                  PartNumber = resource.LinesForPrinting.PartNumber,
                                                                  Qty = resource.LinesForPrinting.Qty,
                                                                  Initials = resource.LinesForPrinting.Initials,
                                                                  Date = resource.LinesForPrinting.Date
                                       },
                                       Printer = resource.Printer,
                                       Quantity = resource.Quantity
                                   };

            var result = this.labelPrintingService.PrintLabel(printDetails);

            return new SuccessResult<LabelPrintResponse>(result);
        }
    }
}
