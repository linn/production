namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRuleResponseProcessor : JsonResponseProcessor<BuildPlanRule>
    {
        public BuildPlanRuleResponseProcessor(IResourceBuilder<BuildPlanRule> resourceBuilder)
            : base(resourceBuilder, "build-plan-rule", 1)
        {
        }
    }
}