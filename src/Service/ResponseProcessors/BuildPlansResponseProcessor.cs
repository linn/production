namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class BuildPlansResponseProcessor : JsonResponseProcessor<IEnumerable<BuildPlan>>
    {
        public BuildPlansResponseProcessor(IResourceBuilder<IEnumerable<BuildPlan>> resourceBuilder)
            : base(resourceBuilder, "build-plans", 1)
        {
        }
    }
}