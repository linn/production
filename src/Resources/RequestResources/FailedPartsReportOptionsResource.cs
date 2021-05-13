namespace Linn.Production.Resources.RequestResources
{
    public class FailedPartsReportOptionsResource : CitCodeRequestResource
    {
        public string PartNumber { get; set; }

        public string OrderByDate { get; set; }
    }
}
