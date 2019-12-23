namespace Linn.Production.Resources.RequestResources
{
    public class AteDetailsReportRequestResource : FromToDateGroupByRequestResource
    {
        public string SmtOrPcb { get; set; }

        public string PlaceFound { get; set; }

        public string SelectBy { get; set; }

        public string Value { get; set; }
    }
}
