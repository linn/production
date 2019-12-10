namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRulesResponseProcessor : JsonResponseProcessor<IEnumerable<BuildPlanRule>>
    {
        public BuildPlanRulesResponseProcessor(IResourceBuilder<IEnumerable<BuildPlanRule>> resourceBuilder)
            : base(resourceBuilder, "build-plan-rules", 1)
        {
        }
    }
}