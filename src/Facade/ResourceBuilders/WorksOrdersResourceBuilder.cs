namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class WorksOrdersResourceBuilder : IResourceBuilder<IEnumerable<WorksOrder>>
    {
        private readonly WorksOrderResourceBuilder resourceBuilder;

        public WorksOrdersResourceBuilder(ISalesArticleService salesArticleService)
        {
            this.resourceBuilder = new WorksOrderResourceBuilder();
        }

        public IEnumerable<WorksOrderResource> Build(IEnumerable<WorksOrder> ateFaultCodes)
        {
            return ateFaultCodes
                .OrderBy(b => b.OrderNumber)
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<WorksOrder>>.Build(IEnumerable<WorksOrder> worksOrders) => this.Build(worksOrders);

        public string GetLocation(IEnumerable<WorksOrder> ateFaultCodes)
        {
            throw new NotImplementedException();
        }
    }
}