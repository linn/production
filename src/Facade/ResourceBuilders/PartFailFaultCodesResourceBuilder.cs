namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailFaultCodesResourceBuilder : IResourceBuilder<IEnumerable<PartFailFaultCode>>
    {
        private readonly PartFailFaultCodeResourceBuilder partFailFaultCodeResourceBuilder = new PartFailFaultCodeResourceBuilder();

        public IEnumerable<PartFailFaultCodeResource> Build(IEnumerable<PartFailFaultCode> partFailFaultCodes)
        {
            return partFailFaultCodes
                .OrderBy(b => b.FaultCode)
                .Select(a => this.partFailFaultCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartFailFaultCode>>.Build(IEnumerable<PartFailFaultCode> partFailFaultCodes) => this.Build(partFailFaultCodes);

        public string GetLocation(IEnumerable<PartFailFaultCode> partFailFaultCodes)
        {
            throw new NotImplementedException();
        }
    }
}