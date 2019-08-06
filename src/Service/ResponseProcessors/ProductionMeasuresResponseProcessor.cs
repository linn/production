namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class ProductionMeasuresResponseProcessor : JsonResponseProcessor<ProductionMeasures>
    {
        public ProductionMeasuresResponseProcessor(IResourceBuilder<ProductionMeasures> resourceBuilder)
            : base(resourceBuilder, "production-measures", 1)
        {
        }
    }
}