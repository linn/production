namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderTimingResponseProcessor : JsonResponseProcessor<WorksOrderTiming>
    {
        public WorksOrderTimingResponseProcessor(IResourceBuilder<WorksOrderTiming> resourceBuilder)
            : base(resourceBuilder, "works-order-timing", 1)
        {
        }
    }
}
