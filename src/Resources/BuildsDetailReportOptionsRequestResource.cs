namespace Linn.Production.Resources
{
    public class BuildsDetailReportOptionsRequestResource
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool Monthly { get; set; }

        public string QuantityOrValue { get; set; }

        public string Department { get; set; }
    }
}