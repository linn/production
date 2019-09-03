namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelsResponseProcessor : JsonResponseProcessor<IEnumerable<ProductionTriggerLevel>>
    {
        public ProductionTriggerLevelsResponseProcessor(IResourceBuilder<IEnumerable<ProductionTriggerLevel>> resourceBuilder)
            : base(resourceBuilder, "production-trigger-levels", 1)
        {
        }
    }
}