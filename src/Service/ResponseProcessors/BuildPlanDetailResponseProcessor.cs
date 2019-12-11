namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanDetailResponseProcessor : JsonResponseProcessor<BuildPlanDetail>
    {
        public BuildPlanDetailResponseProcessor(IResourceBuilder<BuildPlanDetail> resourceBuilder)
            : base(resourceBuilder, "build-plan-detail", 1)
        {
        }
    }
}
