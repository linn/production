namespace Linn.Production.Resources
{
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ShortageResultResource
    {
        public string PartNumber { get; set; }

        public string Priority { get; set; }

        public int? Build { get; set; }

        public int? CanBuild { get; set; }

        public decimal? BackOrderQty { get; set; }

        public int Kanban { get; set; }

        public string EarliestRequestedDate { get; set; }

        public ReportReturnResource Results { get; set; }
    }
}