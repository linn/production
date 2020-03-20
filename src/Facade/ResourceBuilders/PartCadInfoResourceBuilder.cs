namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    // TODO IoC update test imports etc
    public class PartCadInfoResourceBuilder : IResourceBuilder<ResponseModel<PartCadInfo>>
    {
        private readonly IAuthorisationService authorisationService;

        public PartCadInfoResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public PartCadInfoResource Build(ResponseModel<PartCadInfo> model)
        {
            var partCadInfo = model.ResponseData;

            return new PartCadInfoResource
                       {
                           PartNumber = partCadInfo.PartNumber,
                           Description = partCadInfo.Description,
                           FootprintRef = partCadInfo.FootprintRef,
                           LibraryRef = partCadInfo.LibraryRef,
                           MsId = partCadInfo.MsId,
                           LibraryName = partCadInfo.LibraryName,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<PartCadInfo> model)
        {
            return $"/production/maintenance/part-cad-info/{model.ResponseData.MsId}";
        }

        object IResourceBuilder<ResponseModel<PartCadInfo>>.Build(ResponseModel<PartCadInfo> partCadInfo) => this.Build(partCadInfo);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<PartCadInfo> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.PartCadInfoUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "update", Href = this.GetLocation(model) };
            }
        }
    }
}