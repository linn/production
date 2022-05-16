namespace Linn.Production.Resources.RequestResources
{
    public class PartFailDetailsReportRequestResource
    {
        public int? SupplierId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ErrorType { get; set; }

        public string FaultCode { get; set; }

        public string PartNumber { get; set; }

        public string Department { get; set; }
    }
}