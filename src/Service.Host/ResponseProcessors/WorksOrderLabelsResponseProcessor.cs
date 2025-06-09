namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderLabelsResponseProcessor : JsonResponseProcessor<IEnumerable<WorksOrderLabel>>
    {
        public WorksOrderLabelsResponseProcessor(IResourceBuilder<IEnumerable<WorksOrderLabel>> resourceBuilder)
            : base(resourceBuilder, "works-order-labels", 1)
        {
        }
    }
}