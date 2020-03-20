namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartCadInfoResourceBuilder : IResourceBuilder<PartCadInfo>
    {
        public PartCadInfoResource Build(PartCadInfo partCadInfo)
        {
            return new PartCadInfoResource
                       {
                           PartNumber = partCadInfo.PartNumber,
                           Description = partCadInfo.Description,
                           FootprintRef = partCadInfo.FootprintRef,
                           LibraryRef = partCadInfo.LibraryRef,
                           MsId = partCadInfo.MsId
                       };
        }

        public string GetLocation(PartCadInfo partCadInfo)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartCadInfo>.Build(PartCadInfo partCadInfo) => this.Build(partCadInfo);

        private IEnumerable<LinkResource> BuildLinks(PartCadInfo partCadInfo)
        {
            throw new NotImplementedException();
        }
    }
}