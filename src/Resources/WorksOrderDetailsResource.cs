namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class WorksOrderDetailsResource : HypermediaResource
    {
        public string AuditDisclaimer { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string WorkStationCode { get; set; }
    }
}
