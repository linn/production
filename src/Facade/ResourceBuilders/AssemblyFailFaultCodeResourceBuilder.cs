namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

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
                           Description = faultCode.Description
                       };
        }

        public string GetLocation(AssemblyFailFaultCode faultCode)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<AssemblyFailFaultCode>.Build(AssemblyFailFaultCode faultCode) => this.Build(faultCode);

        private IEnumerable<LinkResource> BuildLinks(AssemblyFailFaultCode faultCode)
        {
            throw new NotImplementedException();
        }
    }
}