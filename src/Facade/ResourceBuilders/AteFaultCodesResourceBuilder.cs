namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteFaultCodesResourceBuilder : IResourceBuilder<IEnumerable<AteFaultCode>>
    {
        private readonly AteFaultCodeResourceBuilder ateFaultCodeResourceBuilder = new AteFaultCodeResourceBuilder();

        public IEnumerable<AteFaultCodeResource> Build(IEnumerable<AteFaultCode> ateFaultCodes)
        {
            return ateFaultCodes.Select(a => this.ateFaultCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<AteFaultCode>>.Build(IEnumerable<AteFaultCode> ateFaultCodes) => this.Build(ateFaultCodes);

        public string GetLocation(IEnumerable<AteFaultCode> ateFaultCodes)
        {
            throw new NotImplementedException();
        }
    }
}