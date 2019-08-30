namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ProductionTriggersReportResponseProcessor : JsonResponseProcessor<ProductionTriggersReport>
    {
        public ProductionTriggersReportResponseProcessor(IResourceBuilder<ProductionTriggersReport> resourceBuilder)
            : base(resourceBuilder, "production-measures-list", 1)
        {
        }
    }
}