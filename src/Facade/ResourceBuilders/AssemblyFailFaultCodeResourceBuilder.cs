namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class AssemblyFailFaultCodeResourceBuilder : IResourceBuilder<AssemblyFailFaultCode>
    {
        public AssemblyFailFaultCodeResource Build(AssemblyFailFaultCode faultCode)
        {
            return new AssemblyFailFaultCodeResource
                       {
                           FaultCode = faultCode.FaultCode,
                           Description = faultCode.Description,
                           Explanation = faultCode.Explanation,
                           DateInvalid = faultCode.DateInvalid?.ToString("o"),
                           Links = this.BuildLinks(faultCode).ToArray()
                       };
        }

        public string GetLocation(AssemblyFailFaultCode faultCode)
        {
            return $"/production/quality/assembly-fail-fault-codes/{faultCode.FaultCode}";
        }

        object IResourceBuilder<AssemblyFailFaultCode>.Build(AssemblyFailFaultCode faultCode) => this.Build(faultCode);

        private IEnumerable<LinkResource> BuildLinks(AssemblyFailFaultCode faultCode)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(faultCode) };
        }
    }
}