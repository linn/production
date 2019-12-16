namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRuleResponseProcessor : JsonResponseProcessor<ResponseModel<BuildPlanRule>>
    {
        public BuildPlanRuleResponseProcessor(IResourceBuilder<ResponseModel<BuildPlanRule>> resourceBuilder)
            : base(resourceBuilder, "build-plan-rule", 1)
        {
        }
    }
}