namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class BuildPlansResourceBuilder : IResourceBuilder<IEnumerable<BuildPlan>>
    {
        private readonly BuildPlanResourceBuilder buildPlanResourceBuilder = new BuildPlanResourceBuilder();

        public IEnumerable<BuildPlanResource> Build(IEnumerable<BuildPlan> buildPlans)
        {
            return buildPlans.OrderByDescending(b => b.BuildPlanName)
                .Select(b => this.buildPlanResourceBuilder.Build(b));
        }

        object IResourceBuilder<IEnumerable<BuildPlan>>.Build(IEnumerable<BuildPlan> buildPlans) =>
            this.Build(buildPlans);

        public string GetLocation(IEnumerable<BuildPlan> model)
        {
            throw new System.NotImplementedException();
        }
    }
}