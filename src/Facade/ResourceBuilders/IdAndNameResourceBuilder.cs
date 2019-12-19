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
                Links = this.BuildLinks(idAndName).ToArray()
            };
        }

        public string GetLocation(IdAndName idAndName)
        {
            return string.Empty;
        }

        object IResourceBuilder<IdAndName>.Build(IdAndName idAndName) => this.Build(idAndName);

        private IEnumerable<LinkResource> BuildLinks(IdAndName idAndName)
        {
            return new List<LinkResource>
                       {
                           new LinkResource { Rel = "self", Href = this.GetLocation(idAndName) }
                       };
        }
    }
}
