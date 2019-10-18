namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailsResourceBuilder : IResourceBuilder<IEnumerable<PartFail>>
    {
        private readonly PartFailResourceBuilder partFailResourceBuilder = new PartFailResourceBuilder();

        public IEnumerable<PartFailResource> Build(IEnumerable<PartFail> partFails)
        {
            return partFails.Select(a => this.partFailResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<PartFail> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<PartFail>>.Build(IEnumerable<PartFail> partFails) => this.Build(partFails);
    }
}