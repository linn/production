namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailFaultCodeResourceBuilder : IResourceBuilder<PartFailFaultCode>
    {
        public PartFailFaultCodeResource Build(PartFailFaultCode faultCode)
        {
            return new PartFailFaultCodeResource
                       {
                           FaultCode = faultCode.FaultCode,
                           FaultDescription = faultCode.Description,
                           Links = this.BuildLinks(faultCode).ToArray()
                       };
        }

        public string GetLocation(PartFailFaultCode faultCode)
        {
            return $"/production/quality/part-fail-fault-codes/{faultCode.FaultCode}";
        }

        object IResourceBuilder<PartFailFaultCode>.Build(PartFailFaultCode faultCode) => this.Build(faultCode);

        private IEnumerable<LinkResource> BuildLinks(PartFailFaultCode faultCode)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(faultCode) };
        }
    }
}