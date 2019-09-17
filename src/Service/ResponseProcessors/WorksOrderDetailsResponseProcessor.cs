namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderDetailsResponseProcessor : JsonResponseProcessor<WorksOrderDetails>
    {
        public WorksOrderDetailsResponseProcessor(IResourceBuilder<WorksOrderDetails> resourceBuilder)
            : base(resourceBuilder, "works-order-details", 1)
        {
        }
    }
}