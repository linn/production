namespace Linn.Production.Resources.RequestResources
{
    public class AteDetailsReportRequestResource : AteStatusReportRequestResource
    {
        public string Component { get; set; }

        public string Board { get; set; }

        public string FaultCode { get; set; }
    }
}
