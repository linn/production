namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderPartsDetailsResourceBuilder : IResourceBuilder<WorksOrderPartDetails>
    {
        public WorksOrderPartsDetailsResource Build(WorksOrderPartDetails worksOrderPartDetails)
        {
            return new WorksOrderPartsDetailsResource
                       {
                           PartNumber = worksOrderPartDetails.PartNumber,
                           WorkStationCode = worksOrderPartDetails.WorkStationCode,
                           AuditDisclaimer = worksOrderPartDetails.AuditDisclaimer,
                           PartDescription = worksOrderPartDetails.PartDescription,
                           Links = this.BuildLinks(worksOrderPartDetails).ToArray()
                       };
        }

        public string GetLocation(WorksOrderPartDetails worksOrderPartDetails)
        {
            return $"/production/works-orders/get-part-details/{worksOrderPartDetails.PartNumber}";
        }

        object IResourceBuilder<WorksOrderPartDetails>.Build(WorksOrderPartDetails worksOrderPartDetails) =>
            this.Build(worksOrderPartDetails);

        private IEnumerable<LinkResource> BuildLinks(WorksOrderPartDetails worksOrderPartDetails)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrderPartDetails) };
        }
    }
}