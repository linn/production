namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanResponseProcessor : JsonResponseProcessor<BuildPlan>
    {
        public BuildPlanResponseProcessor(IResourceBuilder<BuildPlan> resourceBuilder)
            : base(resourceBuilder, "build-plan", 1)
        {
        }
    }
}
