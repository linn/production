namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanDetailsResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<BuildPlanDetail>>>
    {
        public BuildPlanDetailsResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<BuildPlanDetail>>> resourceBuilder)
            : base(resourceBuilder, "build-plan-details", 1)
        {
        }
    }
}