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

    public class PartResourceBuilder : IResourceBuilder<ResponseModel<Part>>
    {
        private readonly IAuthorisationService authorisationService;

        public PartResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public PartResource Build(ResponseModel<Part> model)
        {
            var part = model.ResponseData;

            return new PartResource
                       {
                           PartNumber = part.PartNumber,
                           Description = part.Description,
                           BomId = part.BomId,
                           BomType = part.BomType,
                           DecrementRule = part.DecrementRule,
                           LibraryName = part.LibraryName,
                           LibraryRef = part.LibraryRef,
                           FootprintRef = part.FootprintRef,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<Part> model)
        {
            return $"/production/maintenance/parts/{model.ResponseData.PartNumber}";
        }

        object IResourceBuilder<ResponseModel<Part>>.Build(ResponseModel<Part> part) => this.Build(part);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<Part> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.PartUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "update", Href = this.GetLocation(model) };
            }
        }
    }
}