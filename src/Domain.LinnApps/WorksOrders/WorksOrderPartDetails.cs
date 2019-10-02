namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public class WorksOrderPartDetails
    {
        public string AuditDisclaimer { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string WorkStationCode { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentDescription { get; set; }

        public int Quantity { get; set; }
    }
}
