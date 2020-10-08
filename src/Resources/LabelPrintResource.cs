namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class LabelPrintResource : HypermediaResource
    {
        public int LabelType { get; set; }

        public int Printer { get; set; }

        public int Quantity { get; set; }

        public LabelPrintContentsResource LinesForPrinting { get; set; }
    }
}
