namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanDetailResourceBuilder : IResourceBuilder<ResponseModel<BuildPlanDetail>>
    {
        private readonly IAuthorisationService authorisationService;

        public BuildPlanDetailResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public BuildPlanDetailResource Build(ResponseModel<BuildPlanDetail> model)
        {
            var buildPlanDetail = model.ResponseData;

            return new BuildPlanDetailResource
                       {
                           BuildPlanName = buildPlanDetail.BuildPlanName,
                           PartNumber = buildPlanDetail.PartNumber,
                           FromLinnWeekNumber = buildPlanDetail.FromLinnWeekNumber,
                           ToLinnWeekNumber = buildPlanDetail.ToLinnWeekNumber,
                           RuleCode = buildPlanDetail.RuleCode,
                           Quantity = buildPlanDetail.Quantity,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<BuildPlanDetail> model)
        {
            return $"/production/maintenance/build-plan-rules";
        }

        object IResourceBuilder<ResponseModel<BuildPlanDetail>>.Build(ResponseModel<BuildPlanDetail> model) =>
            this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<BuildPlanDetail> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanAdd, model.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = this.GetLocation(model) };

                yield return new LinkResource { Rel = "edit", Href = this.GetLocation(model) };
            }
        }
    }
}
