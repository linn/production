namespace Linn.Production.Domain.LinnApps
{
    public class LabelPrint
    {
        public int LabelType { get; set; }

        public int Printer { get; set; }

        public int Quantity { get; set; }

        public LabelPrintContents LinesForPrinting { get; set; }
    }
}
