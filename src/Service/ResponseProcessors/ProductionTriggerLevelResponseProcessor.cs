namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelResponseProcessor : JsonResponseProcessor<ProductionTriggerLevel>
    {
        public ProductionTriggerLevelResponseProcessor(IResourceBuilder<ProductionTriggerLevel> resourceBuilder)
            : base(resourceBuilder, "production-trigger-level", 1)
        {
        }
    }
}