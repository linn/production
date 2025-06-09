namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    using Nancy.Responses.Negotiation;

    public class BuildPlansResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<BuildPlan>>>
    {
        public BuildPlansResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<BuildPlan>>> resourceBuilder)
            : base(resourceBuilder, "build-plans", 1)
        {
        }
    }
}