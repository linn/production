namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ProductionTriggerFactstResponseProcessor : JsonResponseProcessor<ProductionTriggerFacts>
    {
        public ProductionTriggerFactstResponseProcessor(IResourceBuilder<ProductionTriggerFacts> resourceBuilder)
            : base(resourceBuilder, "production-trigger-facts", 1)
        {
        }
    }
}