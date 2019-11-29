namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelsResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<ProductionTriggerLevel>>>
    {
        public ProductionTriggerLevelsResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<ProductionTriggerLevel>>> resourceBuilder)
            : base(resourceBuilder, "production-trigger-levels", 1)
        {
        }
    }
}