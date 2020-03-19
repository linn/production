namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartResourceBuilder : IResourceBuilder<Part>
    {
        public PartResource Build(Part part)
        {
            return new PartResource
                       {
                           PartNumber = part.PartNumber,
                           Description = part.Description,
                           BomId = part.BomId,
                           BomType = part.BomType,
                           DecrementRule = part.DecrementRule,
                       };
        }

        public string GetLocation(Part part)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Part>.Build(Part part) => this.Build(part);

        private IEnumerable<LinkResource> BuildLinks(Part part)
        {
            throw new NotImplementedException();
        }
    }
}