namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class LabelPrint
    {
        public int LabelType { get; set; }

        public int Printer { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<string> LinesForPrinting { get; set; }
    }
}