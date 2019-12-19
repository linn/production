namespace Linn.Production.Resources.RequestResources
{
    public class AteStatusReportRequestResource : FromToDateGroupByRequestResource
    {
        public string SmtOrPcb { get; set; }

        public string PlaceFound { get; set; }
    }
}
