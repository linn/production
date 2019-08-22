namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class CitsResourceBuilder : IResourceBuilder<IEnumerable<Cit>>
    {
        private readonly CitResourceBuilder citResourceBuilder = new CitResourceBuilder();

        public IEnumerable<CitResource> Build(IEnumerable<Cit> cits)
        {
            return cits.Select(a => this.citResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Cit>>.Build(IEnumerable<Cit> cits) => this.Build(cits);

        public string GetLocation(IEnumerable<Cit> cits)
        {
            throw new NotImplementedException();
        }
    }
}