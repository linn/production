namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class LabelPrintResource : HypermediaResource
    {
        public int LabelType { get; set; }

        public int Printer { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<string> LinesForPrinting { get; set; }
    }
}
