namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderPartDetailsResponseProcessor : JsonResponseProcessor<WorksOrderPartDetails>
    {
        public WorksOrderPartDetailsResponseProcessor(IResourceBuilder<WorksOrderPartDetails> resourceBuilder)
            : base(resourceBuilder, "works-order-parts-details", 1)
        {
        }
    }
}