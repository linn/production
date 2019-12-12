namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanDetailResourceBuilder : IResourceBuilder<BuildPlanDetail>
    {
        public BuildPlanDetailResource Build(BuildPlanDetail buildPlanDetail)
        {
            return new BuildPlanDetailResource
                       {
                           BuildPlanName = buildPlanDetail.BuildPlanName,
                           PartNumber = buildPlanDetail.PartNumber,
                           FromLinnWeekNumber = buildPlanDetail.FromLinnWeekNumber,
                           ToLinnWeekNumber = buildPlanDetail.ToLinnWeekNumber,
                           RuleCode = buildPlanDetail.RuleCode,
                           Quantity = buildPlanDetail.Quantity
                       };
        }

        public string GetLocation(BuildPlanDetail buildPlanDetail)
        {
            return $"/production/maintenance/build-plan-rules";
        }

        object IResourceBuilder<BuildPlanDetail>.Build(BuildPlanDetail buildPlanDetail) => this.Build(buildPlanDetail);

        private IEnumerable<LinkResource> BuildLinks(BuildPlanDetail buildPlanDetail)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(buildPlanDetail) };
        }
    }
}
