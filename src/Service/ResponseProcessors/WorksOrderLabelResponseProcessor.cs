namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderLabelResponseProcessor : JsonResponseProcessor<WorksOrderLabel>
    {
        public WorksOrderLabelResponseProcessor(IResourceBuilder<WorksOrderLabel> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}