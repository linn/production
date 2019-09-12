namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderResourceBuilder : IResourceBuilder<WorksOrder>
    {
        public WorksOrderResource Build(WorksOrder wo)
        {
            return new WorksOrderResource
            {
                OrderNumber = wo.OrderNumber,
                PartNumber = wo.PartNumber,
                PartDescription = wo.Part.Description
            };
        }

        public string GetLocation(WorksOrder worksOrder)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<WorksOrder>.Build(WorksOrder w) => this.Build(w);

        private IEnumerable<LinkResource> BuildLinks(WorksOrder worksOrder)
        {
            throw new NotImplementedException();
        }
    }
}