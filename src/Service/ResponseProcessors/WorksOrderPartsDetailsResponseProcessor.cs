namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderPartsDetailsResponseProcessor : JsonResponseProcessor<WorksOrderPartDetails>
    {
        public WorksOrderPartsDetailsResponseProcessor(IResourceBuilder<WorksOrderPartDetails> resourceBuilder)
            : base(resourceBuilder, "works-order-parts-details", 1)
        {
        }
    }
}