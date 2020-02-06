namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class IdAndNameResourceBuilder : IResourceBuilder<IdAndName>
    {
        public IdAndNameResource Build(IdAndName idAndName)
        {
            return new IdAndNameResource
            {
                Id = idAndName.Id,
                Name = idAndName.Name,
                Links = this.BuildLinks().ToArray()
            };
        }

        public string GetLocation(IdAndName idAndName)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IdAndName>.Build(IdAndName idAndName) => this.Build(idAndName);

        private IEnumerable<LinkResource> BuildLinks()
        {
            return new List<LinkResource>();
        }
    }
}
