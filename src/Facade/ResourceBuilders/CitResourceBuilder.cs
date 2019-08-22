namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class CitResourceBuilder : IResourceBuilder<Cit>
    {
        public CitResource Build(Cit cit)
        {
            return new CitResource
                       {
                           Code = cit.Code,
                           Name = cit.Name
                       };
        }

        public string GetLocation(Cit cit)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Cit>.Build(Cit cit) => this.Build(cit);

        private IEnumerable<LinkResource> BuildLinks(Cit cit)
        {
            throw new NotImplementedException();
        }
    }
}