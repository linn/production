namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;
    using System.Collections.Generic;

    public class AssemblyFailFaultCode
    {
        public string FaultCode { get; set; }

        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }

        public string Explanation { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }
    }
}
