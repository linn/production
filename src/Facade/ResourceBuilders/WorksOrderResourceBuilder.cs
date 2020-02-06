namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderResourceBuilder : IResourceBuilder<WorksOrder>
    {
        public WorksOrderResource Build(WorksOrder worksOrder)
        {
            return new WorksOrderResource
                       {
                           BatchNumber = worksOrder.BatchNumber,
                           CancelledBy = worksOrder.CancelledBy,
                           DateCancelled = worksOrder.DateCancelled?.ToString("o"),
                           DateRaised = worksOrder.DateRaised.ToString("o"),
                           KittedShort = worksOrder.KittedShort,
                           LabelsPrinted = worksOrder.LabelsPrinted,
                           OrderNumber = worksOrder.OrderNumber,
                           Outstanding = worksOrder.Outstanding,
                           PartNumber = worksOrder.PartNumber,
                           PartDescription = worksOrder.Part.Description,
                           Quantity = worksOrder.Quantity,
                           QuantityBuilt = worksOrder.QuantityBuilt,
                           QuantityOutstanding = worksOrder.QuantityOutstanding,
                           RaisedBy = worksOrder.RaisedBy,
                           RaisedByDepartment = worksOrder.RaisedByDepartment,
                           ReasonCancelled = worksOrder.ReasonCancelled,
                           StartedByShift = worksOrder.StartedByShift,
                           DocType = worksOrder.DocType,
                           WorkStationCode = worksOrder.WorkStationCode,
                           BatchNotes = worksOrder.BatchNotes,
                           Links = this.BuildLinks(worksOrder).ToArray(),
                           QtyTested = worksOrder.AteTests?.Sum(t => t.NumberTested) ?? 0
                       };
        }

        public string GetLocation(WorksOrder worksOrder)
        {
            return $"/production/works-orders/{worksOrder.OrderNumber}";
        }

        object IResourceBuilder<WorksOrder>.Build(WorksOrder worksOrder) => this.Build(worksOrder);

        private IEnumerable<LinkResource> BuildLinks(WorksOrder worksOrder)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrder) };
        }
    }
}
