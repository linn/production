namespace Linn.Production.Resources.RequestResources
{
    public class WorksOrderRequestResource : SearchRequestResource
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string PartNumber { get; set; }
    }
}
