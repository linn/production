namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderTimingsResourceBuilder : IResourceBuilder<IEnumerable<WorksOrderTiming>>
    {
        private readonly WorksOrderTimingResourceBuilder worksOrderTimingResourceBuilder = new WorksOrderTimingResourceBuilder();

        public IEnumerable<WorksOrderTimingResource> Build(IEnumerable<WorksOrderTiming> worksOrderTimings)
        {
            return worksOrderTimings
                .OrderByDescending(w => w.OrderNumber)
                .Select(w => this.worksOrderTimingResourceBuilder.Build(w));
        }

        object IResourceBuilder<IEnumerable<WorksOrderTiming>>.Build(IEnumerable<WorksOrderTiming> worksOrderTimings) =>
            this.Build(worksOrderTimings);

        public string GetLocation(IEnumerable<WorksOrderTiming> model)  
        {
            throw new System.NotImplementedException();
        }
    }
}
