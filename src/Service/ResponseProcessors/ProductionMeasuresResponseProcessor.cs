using Linn.Common.Facade;
using Linn.Common.Nancy.Facade;
using Linn.Production.Domain;

namespace Linn.Production.Service.ResponseProcessors
{
    public class ProductionMeasuresResponseProcessor : JsonResponseProcessor<ProductionMeasures>
    {
        public ProductionMeasuresResponseProcessor(IResourceBuilder<ProductionMeasures> resourceBuilder)
            : base(resourceBuilder, "production-measures", 1)
        {
        }
    }
}