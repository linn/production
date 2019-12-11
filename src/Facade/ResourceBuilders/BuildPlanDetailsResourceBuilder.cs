namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanDetailsResourceBuilder : IResourceBuilder<IEnumerable<BuildPlanDetail>>
    {
        private readonly BuildPlanDetailResourceBuilder buildPlanDetailResourceBuilder =
            new BuildPlanDetailResourceBuilder();

        public IEnumerable<BuildPlanDetailResource> Build(IEnumerable<BuildPlanDetail> buildPlanDetails)
        {
            return buildPlanDetails.Select(b => this.buildPlanDetailResourceBuilder.Build(b));
        }

        object IResourceBuilder<IEnumerable<BuildPlanDetail>>.Build(IEnumerable<BuildPlanDetail> buildPlanDetails) =>
            this.Build(buildPlanDetails);

        public string GetLocation(IEnumerable<BuildPlanDetail> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
