namespace Linn.Production.Resources.RequestResources
{
    public class OverdueOrdersReportRequestResource
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string AccountingCompany { get; set; }

        public string StockPool { get; set; }

        public string ReportBy { get; set; }

        public string DaysMethod { get; set; }
    }
}
