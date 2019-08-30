namespace Linn.Production.Facade.ResourceBuilders
{
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
            return cits.Select(c => this.citResourceBuilder.Build(c));
        }

        object IResourceBuilder<IEnumerable<Cit>>.Build(IEnumerable<Cit> cits) => this.Build(cits);

        public string GetLocation(IEnumerable<Cit> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
