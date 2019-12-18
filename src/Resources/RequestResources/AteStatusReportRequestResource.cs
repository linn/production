namespace Linn.Production.Resources.RequestResources
{
    public class AteStatusReportRequestResource : FromToDateRequestResource
    {
        public string SmtOrPcb { get; set; }

        public string PlaceFound { get; set; }
    }
}
