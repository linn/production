namespace Linn.Production.Domain.LinnApps.Measures
{
    using System.Collections.Generic;

    public class AssemblyFailFaultCode
    {
        public string FaultCode { get; set; }

        public string Description { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }
    }
}
