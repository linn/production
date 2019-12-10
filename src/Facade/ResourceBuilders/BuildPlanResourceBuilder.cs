namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanResourceBuilder : IResourceBuilder<BuildPlan>
    {
        public BuildPlanResource Build(BuildPlan buildPlan)
        {
            return new BuildPlanResource
                       {
                           BuildPlanName = buildPlan.BuildPlanName,
                           DateCreated = buildPlan.DateCreated.ToString("o"),
                           DateInvalid = buildPlan.DateInvalid?.ToString("o"),
                           Description = buildPlan.Description,
                           LastMrpDateFinished = buildPlan.LastMrpDateFinished?.ToString("o"),
                           LastMrpDateStarted = buildPlan.LastMrpDateStarted?.ToString("o"),
                           LastMrpJobRef = buildPlan.LastMrpJobRef
                       };
        }

        public string GetLocation(BuildPlan buildPlan)
        {
            return $"/production/maintenance/build-plans/{buildPlan.BuildPlanName}";
        }

        object IResourceBuilder<BuildPlan>.Build(BuildPlan buildPlan) => this.Build(buildPlan);

        private IEnumerable<LinkResource> BuildLinks(BuildPlan buildPlan)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(buildPlan) };
        }
    }
}