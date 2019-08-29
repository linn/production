namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WorksOrderResourceBuilder : IResourceBuilder<WorksOrder>
    {
        public WorksOrderResource Build(WorksOrder worksOrder)
        {
            return new WorksOrderResource
                       {
                           CancelledBy = worksOrder.CancelledBy,
                           DateCancelled = worksOrder.DateCancelled.ToString("o"),
                           DateRaised = worksOrder.DateRaised.ToString("o"),
                           LabelsPrinted = worksOrder.LabelsPrinted,
                           OrderNumber = worksOrder.OrderNumber,
                           PartNumber = worksOrder.PartNumber,
                           QuantityBuilt = worksOrder.QuantityBuilt,
                           QuantityOutstanding = worksOrder.QuantityOutstanding,
                           RaisedBy = worksOrder.RaisedBy,
                           RaisedByDepartment = worksOrder.RaisedByDepartment,
                           ReasonCancelled = worksOrder.ReasonCancelled,
                           Type = worksOrder.Type,
                           WorkStationCode = worksOrder.WorkStationCode,
                           Links = this.BuildLinks(worksOrder).ToArray()
                       };
        }

        public string GetLocation(WorksOrder worksOrder)
        {
            return $"/production/maintenance/works-orders/{worksOrder.OrderNumber}";
        }

        object IResourceBuilder<WorksOrder>.Build(WorksOrder worksOrder) => this.Build(worksOrder);

        private IEnumerable<LinkResource> BuildLinks(WorksOrder worksOrder)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrder) };
        }
    }
}
