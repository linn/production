namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class LabelPrint
    {
        public int LabelType { get; set; }

        public int Printer { get; set; }

        public int Quantity { get; set; }

        public LabelPrintContents LinesForPrinting { get; set; }
    }
}
