namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class LabelPrintResponse
    {
        public LabelPrintResponse(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
