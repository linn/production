namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderTimingResourceBuilder : IResourceBuilder<WorksOrderTiming>
    {
        public WorksOrderTimingResource Build(WorksOrderTiming worksOrderTiming)
        {
            return new WorksOrderTimingResource
            {
                OrderNumber = worksOrderTiming.OrderNumber,
                OperationNumber = worksOrderTiming.OperationNumber, 
                OperationType = worksOrderTiming.OperationType,
                BuiltBy = worksOrderTiming.BuiltBy,
                ResourceCode = worksOrderTiming.ResourceCode,
                RouteCode = worksOrderTiming.RouteCode,
                StartTime = worksOrderTiming.StartTime,
                EndTime = worksOrderTiming.EndTime,
                TimeTaken = worksOrderTiming.TimeTaken
            };
        }

        public string GetLocation(WorksOrderTiming worksOrderTiming)
        {
            return $"/production/works-order-timing/{worksOrderTiming.OrderNumber}";
        }

        object IResourceBuilder<WorksOrderTiming>.Build(WorksOrderTiming worksOrderTiming) => this.Build(worksOrderTiming);

        private IEnumerable<LinkResource> BuildLinks(WorksOrderTiming worksOrderTiming)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrderTiming) };
        }
    }
}
