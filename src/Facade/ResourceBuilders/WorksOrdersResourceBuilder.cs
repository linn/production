namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrdersResourceBuilder : IResourceBuilder<IEnumerable<WorksOrder>>
    {
        private readonly WorksOrderResourceBuilder worksOrderResourceBuilder = new WorksOrderResourceBuilder();

        public IEnumerable<WorksOrderResource> Build(IEnumerable<WorksOrder> worksOrders)
        {
            return worksOrders
                .OrderByDescending(w => w.OrderNumber)
                .Select(w => this.worksOrderResourceBuilder.Build(w));
        }

        object IResourceBuilder<IEnumerable<WorksOrder>>.Build(IEnumerable<WorksOrder> worksOrders) =>
            this.Build(worksOrders);

        public string GetLocation(IEnumerable<WorksOrder> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
