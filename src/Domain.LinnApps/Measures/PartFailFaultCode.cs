namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;
    using System.Collections.Generic;

    public class PartFailFaultCode
    {
        public string FaultCode { get; set; }

        public DateTime? DateInvalid { get; set; }

        public string Description { get; set; }

        public List<PartFail> PartFails { get; set; }
    }
}