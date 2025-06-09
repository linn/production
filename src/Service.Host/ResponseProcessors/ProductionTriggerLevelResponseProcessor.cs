namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelResponseProcessor : JsonResponseProcessor<ResponseModel<ProductionTriggerLevel>>
    {
        public ProductionTriggerLevelResponseProcessor(IResourceBuilder<ResponseModel<ProductionTriggerLevel>> resourceBuilder)
            : base(resourceBuilder, "production-trigger-level", 1)
        {
        }
    }
}