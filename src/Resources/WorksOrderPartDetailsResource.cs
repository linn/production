namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class WorksOrderPartDetailsResource : HypermediaResource
    {
        public string AuditDisclaimer { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string WorkStationCode { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentDescription { get; set; }
    }
}
