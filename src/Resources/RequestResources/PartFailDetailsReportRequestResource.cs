namespace Linn.Production.Resources.RequestResources
{
    public class PartFailDetailsReportRequestResource
    {
        public int? SupplierId { get; set; }

        public string FromWeek { get; set; }

        public string ToWeek { get; set; }

        public string ErrorType { get; set; }

        public string FaultCode { get; set; }

        public string PartNumber { get; set; }

        public string Department { get; set; }
    }
}