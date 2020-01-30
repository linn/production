namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class LabelPrintingService : ILabelPrintingService
    {
        private readonly ILabelService labelService;

        public LabelPrintingService(
            ILabelService labelService)
        {
            this.labelService = labelService;
        }
        
        public LabelPrintResponse PrintLabel(LabelPrint resource)
        {
            var labelType = (GeneralPurposeLabelTypes.Labels)resource.LabelType;

            var dateTimeNow = DateTime.Now.ToString("ddMMMyyyyHH''mm''ss");
            var printer = ((LabelPrinters.Printers)resource.Printer).ToString();

            var printMapper = new Dictionary<GeneralPurposeLabelTypes.Labels, Func<LabelPrint, string, string, LabelPrintResponse>>
                              {
                                  { GeneralPurposeLabelTypes.Labels.PCNumbers, this.PrintPcNumbers },
                                  { GeneralPurposeLabelTypes.Labels.AddressLabel, this.PrintAddressLabel },
                                  { GeneralPurposeLabelTypes.Labels.Small, this.PrintSmallLabel },
                                  { GeneralPurposeLabelTypes.Labels.SmallWeeText, this.PrintSmallWeeTextLabel },
                                  { GeneralPurposeLabelTypes.Labels.SmallBoldText, this.PrintSmallWeeBoldTextLabel },
                                  { GeneralPurposeLabelTypes.Labels.GoodsInLabel, this.PrintGoodsInLabel },
                                  { GeneralPurposeLabelTypes.Labels.LargeBigText, this.PrintLargeBigTextLabel },
                                  { GeneralPurposeLabelTypes.Labels.LargeWeeText, this.PrintLargeWeeTextLabel }
                              };

            var result = printMapper[labelType](resource, dateTimeNow, printer);

            return result;
        }

        private LabelPrintResponse PrintPcNumbers(LabelPrint resource, string dateTimeNow, string printer)
        {
            if (!string.IsNullOrWhiteSpace(resource.LinesForPrinting.FromPCNumber))
            {
                var fromString = resource.LinesForPrinting.FromPCNumber;
                var from = int.Parse(fromString);

                var to = int.Parse(
                    string.IsNullOrWhiteSpace(resource.LinesForPrinting.ToPCNumber)
                        ? fromString
                        : resource.LinesForPrinting.ToPCNumber);

                for (int pcNumber = from; pcNumber <= to; pcNumber++)
                {
                    this.labelService.PrintLabel(
                        $"PC{dateTimeNow}",
                        printer,
                        resource.Quantity,
                        "c:\\lbl\\PCLabel.btw",
                        pcNumber.ToString());
                }

                return new LabelPrintResponse(
                        $"printed pc numbers {from} to {to}");
            }

            throw new ArgumentException("No PC number provided");
        }

        private LabelPrintResponse PrintSmallLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            var data = string.IsNullOrWhiteSpace(resource.LinesForPrinting.Line2)
                           ? resource.LinesForPrinting.Line1
                           : $"\"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\"";

            this.labelService.PrintLabel($"S{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel.btw", data);

            return new LabelPrintResponse($"printed small label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintSmallWeeTextLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            this.labelService.PrintLabel($"SW{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3.btw", resource.LinesForPrinting.Line1);

            return new LabelPrintResponse($"printed small (wee text) label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintSmallWeeBoldTextLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            this.labelService.PrintLabel($"SWB{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genSmallLabel3b.btw", resource.LinesForPrinting.Line1);

            return new LabelPrintResponse($"printed small (wee bold text) label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintAddressLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.SupplierId}\", \"{resource.LinesForPrinting.AddressId}\", \"{resource.LinesForPrinting.Addressee}\","
                + $"\"{resource.LinesForPrinting.Addressee2}\", \"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\","
                + $" \"{resource.LinesForPrinting.Line3}\", \"{resource.LinesForPrinting.Line4}\", \"{resource.LinesForPrinting.PostalCode}\","
                + $" \"{resource.LinesForPrinting.Country}\"";

            this.labelService.PrintLabel($"ADDR{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genAddressLabel.btw", data);

            return new LabelPrintResponse($"printed address label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintGoodsInLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.SupplierId}\", \"{resource.LinesForPrinting.AddressId}\", \"{resource.LinesForPrinting.PoNumber}\","
                + $"\"{resource.LinesForPrinting.PartNumber}\", \"{resource.LinesForPrinting.Qty}\", \"{resource.LinesForPrinting.Initials}\","
                + $" \"{resource.LinesForPrinting.Date}\"";

            this.labelService.PrintLabel($"GI{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\goods_in_2004.btw", data);

            return new LabelPrintResponse($"printed goods in label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintLargeBigTextLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            
            this.labelService.PrintLabel($"L1{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genLargeLabel_1line.btw", resource.LinesForPrinting.Line1);

            return new LabelPrintResponse($"printed large (big text) label{(resource.Quantity != 1 ? "s" : "")}");
        }

        private LabelPrintResponse PrintLargeWeeTextLabel(LabelPrint resource, string dateTimeNow, string printer)
        {
            var data =
                $"\"{resource.LinesForPrinting.Line1}\", \"{resource.LinesForPrinting.Line2}\", \"{resource.LinesForPrinting.Line3}\","
                + $"\"{resource.LinesForPrinting.Line4}\", \"{resource.LinesForPrinting.Line5}\", \"{resource.LinesForPrinting.Line6}\","
                + $" \"{resource.LinesForPrinting.Line7}\"";

            this.labelService.PrintLabel($"L{dateTimeNow}", printer, resource.Quantity, "c:\\lbl\\genLargeLabel.btw", data);

            return new LabelPrintResponse($"printed large (wee text) label{(resource.Quantity != 1 ? "s" : "")}");
        }
    }
}
