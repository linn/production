namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class MechPartSourceResourceBuilder : IResourceBuilder<MechPartSource>
    {
        public MechPartSourceResource Build(MechPartSource mechPartSource)
        {
            return new MechPartSourceResource
                       {
                           PartNumber = mechPartSource.PartNumber,
                           Description = mechPartSource.Description,
                           FootprintRef = mechPartSource.FootprintRef,
                           LibraryRef = mechPartSource.LibraryRef,
                           MsId = mechPartSource.MsId
                       };
        }

        public string GetLocation(MechPartSource mechPartSource)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<MechPartSource>.Build(MechPartSource mechPartSource) => this.Build(mechPartSource);

        private IEnumerable<LinkResource> BuildLinks(MechPartSource mechPartSource)
        {
            throw new NotImplementedException();
        }
    }
}