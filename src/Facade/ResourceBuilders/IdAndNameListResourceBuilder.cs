namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class IdAndNameListResourceBuilder : IResourceBuilder<IEnumerable<IdAndName>>
    {
        private readonly IdAndNameResourceBuilder idAndNameResourceBuilder = new IdAndNameResourceBuilder();

        public IEnumerable<IdAndNameResource> Build(IEnumerable<IdAndName> list)
        {
            return list
                .OrderBy(b => b.Id)
                .Select(a => this.idAndNameResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<IdAndName>>.Build(IEnumerable<IdAndName> list) => this.Build(list);

        public string GetLocation(IEnumerable<IdAndName> list)
        {
            throw new NotImplementedException();
        }
    }
}
