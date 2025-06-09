namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanDetailResponseProcessor : JsonResponseProcessor<ResponseModel<BuildPlanDetail>>
    {
        public BuildPlanDetailResponseProcessor(IResourceBuilder<ResponseModel<BuildPlanDetail>> resourceBuilder)
            : base(resourceBuilder, "build-plan-detail", 1)
        {
        }
    }
}
