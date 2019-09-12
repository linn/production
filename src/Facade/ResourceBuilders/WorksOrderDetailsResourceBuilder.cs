namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderDetailsResourceBuilder : IResourceBuilder<WorksOrderDetails>
    {
        public WorksOrderDetailsResource Build(WorksOrderDetails worksOrderDetails)
        {
            return new WorksOrderDetailsResource
                       {
                           PartNumber = worksOrderDetails.PartNumber,
                           WorkStationCode = worksOrderDetails.WorkStationCode,
                           AuditDisclaimer = worksOrderDetails.AuditDisclaimer,
                           PartDescription = worksOrderDetails.PartDescription,
                           Links = this.BuildLinks(worksOrderDetails).ToArray()
                       };
        }

        public string GetLocation(WorksOrderDetails worksOrderDetails)
        {
            return $"/production/maintenance/works-orders/details/{worksOrderDetails.PartNumber}";
        }

        object IResourceBuilder<WorksOrderDetails>.Build(WorksOrderDetails worksOrderDetails) =>
            this.Build(worksOrderDetails);

        private IEnumerable<LinkResource> BuildLinks(WorksOrderDetails worksOrderDetails)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrderDetails) };
        }
    }
}