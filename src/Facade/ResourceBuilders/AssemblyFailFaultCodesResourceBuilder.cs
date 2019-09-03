namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class AssemblyFailFaultCodesResourceBuilder : IResourceBuilder<IEnumerable<AssemblyFailFaultCode>>
    {
        private readonly AssemblyFailFaultCodeResourceBuilder assemblyFailFaultCodeResourceBuilder = new AssemblyFailFaultCodeResourceBuilder();

        public IEnumerable<AssemblyFailFaultCodeResource> Build(IEnumerable<AssemblyFailFaultCode> assemblyFailFaultCodes)
        {
            return assemblyFailFaultCodes
                .OrderBy(b => b.FaultCode)
                .Select(a => this.assemblyFailFaultCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<AssemblyFailFaultCode>>.Build(IEnumerable<AssemblyFailFaultCode> assemblyFailFaultCodes) => this.Build(assemblyFailFaultCodes);

        public string GetLocation(IEnumerable<AssemblyFailFaultCode> assemblyFailFaultCodes)
        {
            throw new NotImplementedException();
        }
    }
}