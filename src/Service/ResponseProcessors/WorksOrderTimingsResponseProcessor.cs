namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderTimingsResponseProcessor : JsonResponseProcessor<IEnumerable<WorksOrderTiming>>
    {
        public WorksOrderTimingsResponseProcessor(IResourceBuilder<IEnumerable<WorksOrderTiming>> resourceBuilder)
            : base(resourceBuilder, "works-order-timings", 1)
        {
        }
    }
}
