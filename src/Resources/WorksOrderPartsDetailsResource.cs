namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class WorksOrderPartsDetailsResource : HypermediaResource
    {
        public string AuditDisclaimer { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string WorkStationCode { get; set; }
    }
}
