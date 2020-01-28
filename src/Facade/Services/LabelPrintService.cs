﻿namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources;

    public class LabelPrintService : ILabelPrintService
    {
        private readonly ILabelService labelService;

        public LabelPrintService(
            ILabelService labelService)
        {
            this.labelService = labelService;
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

        public IResult<LabelPrintResponse> PrintLabel(LabelPrintResource resource)
        {
            var labelType = (GeneralPurposeLabelTypes.Labels)resource.LabelType;

            var dateTimeNow = DateTime.Now.ToString("ddMMMyyyyHH''mm''ss");
            var printer = ((LabelPrinters.Printers)resource.Printer).ToString();

            var printMapper = new Dictionary<GeneralPurposeLabelTypes.Labels, IResult<LabelPrintResponse>>
                              {
                                  { GeneralPurposeLabelTypes.Labels.PCNumbers, this.PrintPcNumbers(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.AddressLabel, this.PrintAddressLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.Small, this.PrintSmallLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.SmallWeeText, this.PrintSmallWeeTextLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.SmallBoldText, this.PrintSmallWeeBoldTextLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.GoodsInLabel, this.PrintGoodsInLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.LargeBigText, this.PrintLargeBigTextLabel(resource, dateTimeNow, printer) },
                                  { GeneralPurposeLabelTypes.Labels.LargeWeeText, this.PrintLargeWeeTextLabel(resource, dateTimeNow, printer) }
                              };

            var result = printMapper[labelType];

            return result;
        }

        private IResult<LabelPrintResponse> PrintPcNumbers(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            var from = int.Parse(resource.LinesForPrinting.FromPCNumber);
            var to = int.Parse(resource.LinesForPrinting.ToPCNumber);
            for (int pcNumber = from; pcNumber <= to; pcNumber++)
            {

                this.labelService.PrintLabel(
                    $"PC{dateTimeNow}",
                    printer,
                    resource.Quantity,
                    "c:\\lbl\\PCLabel.btw",
                    pcNumber.ToString());
            }

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed pc numbers {resource.LinesForPrinting.FromPCNumber} to {resource.LinesForPrinting.ToPCNumber} ({resource.Quantity} times)"));
        }

        private IResult<LabelPrintResponse> PrintSmallLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            var data = string.IsNullOrWhiteSpace(resource.LinesForPrinting.Line2)
                           ? resource.LinesForPrinting.Line1
                           : $"\"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\"";

            this.labelService.PrintLabel($"S{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel.btw", data);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintSmallWeeTextLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            this.labelService.PrintLabel($"SW{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\SMTLabels.btw", resource.LinesForPrinting.Line1);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintSmallWeeBoldTextLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            this.labelService.PrintLabel($"SWB{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3b.btw", resource.LinesForPrinting.Line1);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintAddressLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.SupplierId}\", \"{resource.LinesForPrinting.AddressId}\", \"{resource.LinesForPrinting.Addressee}\","
                + $"\"{resource.LinesForPrinting.Addressee2}\", \"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\","
                + $" \"{resource.LinesForPrinting.Line3}\", \"{resource.LinesForPrinting.Line4}\", \"{resource.LinesForPrinting.PostalCode}\","
                + $" \"{resource.LinesForPrinting.Country}\", ";

            this.labelService.PrintLabel($"ADDR{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel.btw", data);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintGoodsInLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.SupplierId}\", \"{resource.LinesForPrinting.AddressId}\", \"{resource.LinesForPrinting.PoNumber}\","
                + $"\"{resource.LinesForPrinting.PartNumber}\", \"{resource.LinesForPrinting.Qty}\", \"{resource.LinesForPrinting.Initials}\","
                + $" \"{resource.LinesForPrinting.Date}\"";

            this.labelService.PrintLabel($"GI{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3b.btw", data);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintLargeBigTextLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            this.labelService.PrintLabel($"L{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3b.btw", resource.LinesForPrinting.Line1);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }

        private IResult<LabelPrintResponse> PrintLargeWeeTextLabel(LabelPrintResource resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\", \"{resource.LinesForPrinting.Line3}\","
                + $"\"{resource.LinesForPrinting.Line4}\", \"{resource.LinesForPrinting.Line5}\", \"{resource.LinesForPrinting.Line6}\","
                + $" \"{resource.LinesForPrinting.Line7}\"";

            this.labelService.PrintLabel($"L1{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3b.btw", data);

            return new SuccessResult<LabelPrintResponse>(new LabelPrintResponse($"printed address label {resource.Quantity} times"));
        }
    }
}
