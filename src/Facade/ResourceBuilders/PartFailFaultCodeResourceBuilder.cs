namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

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
                           FaultDescription = faultCode.Description
                       };
        }

        public string GetLocation(PartFailFaultCode faultCode)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartFailFaultCode>.Build(PartFailFaultCode faultCode) => this.Build(faultCode);

        private IEnumerable<LinkResource> BuildLinks(PartFailFaultCode faultCode)
        {
            throw new NotImplementedException();
        }
    }
}