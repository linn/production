namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelReprintResourceBuilder : IResourceBuilder<ResponseModel<LabelReprint>>
    {
        private readonly IAuthorisationService authorisationService;

        public LabelReprintResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public LabelReprintResource Build(ResponseModel<LabelReprint> responseModel)
        {
            var model = responseModel.ResponseData;
            return new LabelReprintResource
                       {
                           DateIssued = model.DateIssued.ToString("o"),
                           LabelReprintId = model.LabelReprintId,
                           DocumentType = model.DocumentType,
                           WorksOrderNumber = model.WorksOrderNumber,
                           NewPartNumber = model.NewPartNumber,
                           Reason = model.Reason,
                           NumberOfProducts = model.NumberOfProducts,
                           LabelTypeCode = model.LabelTypeCode,
                           SerialNumber = model.SerialNumber,
                           PartNumber = model.PartNumber,
                           ReprintType = model.ReprintType,
                           Links = this.BuildLinks(responseModel).ToArray()
                       };
        }

        object IResourceBuilder<ResponseModel<LabelReprint>>.Build(ResponseModel<LabelReprint> labelReprint) => this.Build(labelReprint);

        public string GetLocation(ResponseModel<LabelReprint> labelReprint)
        {
            return $"/production/maintenance/labels/reprint-reasons/{labelReprint.ResponseData.LabelReprintId}";
        }

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<LabelReprint> labelReprint)
        {
            if (labelReprint.ResponseData.LabelReprintId > 0)
            {
                yield return new LinkResource { Rel = "self", Href = this.GetLocation(labelReprint) };

                yield return new LinkResource("requested-by", $"/employees/{labelReprint.ResponseData.RequestedBy}");
            }

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.SerialNumberReissueRebuild, labelReprint.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = "/production/maintenance/labels/reprint-reasons" };
            }
        }
    }
}