namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class BuildPlanDetailResourceBuilder : IResourceBuilder<ResponseModel<BuildPlanDetail>>
    {
        private readonly IAuthorisationService authorisationService;

        private readonly ILinnWeekPack linnWeekPack;

        public BuildPlanDetailResourceBuilder(IAuthorisationService authorisationService, ILinnWeekPack linnWeekPack)
        {
            this.authorisationService = authorisationService;
            this.linnWeekPack = linnWeekPack;
        }

        public BuildPlanDetailResource Build(ResponseModel<BuildPlanDetail> model)
        {
            var buildPlanDetail = model.ResponseData;

            return new BuildPlanDetailResource
                       {
                           BuildPlanName = buildPlanDetail.BuildPlanName,
                           PartNumber = buildPlanDetail.PartNumber,
                           FromDate = this.linnWeekPack.LinnWeekStartDate(buildPlanDetail.FromLinnWeekNumber).ToString("o"),
                           ToDate = buildPlanDetail.ToLinnWeekNumber == null || buildPlanDetail.ToLinnWeekNumber < 0
                                        ? null
                                        : this.linnWeekPack.LinnWeekStartDate((int)buildPlanDetail.ToLinnWeekNumber)
                                            .ToString("o"),
                           RuleCode = buildPlanDetail.RuleCode,
                           Quantity = buildPlanDetail.Quantity,
                           PartDescription = buildPlanDetail.Part?.Description,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<BuildPlanDetail> model)
        {
            return "/production/maintenance/build-plan-details";
        }

        object IResourceBuilder<ResponseModel<BuildPlanDetail>>.Build(ResponseModel<BuildPlanDetail> model) =>
            this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<BuildPlanDetail> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanDetailAdd, model.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = this.GetLocation(model) };
            }

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanDetailUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "update", Href = this.GetLocation(model) };
            }
        }
    }
}
