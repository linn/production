namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderResponseProcessor : JsonResponseProcessor<WorksOrder>
    {
        public WorksOrderResponseProcessor(IResourceBuilder<WorksOrder> resourceBuilder)
            : base(resourceBuilder, "works-order", 1)
        {
        }
    }
}