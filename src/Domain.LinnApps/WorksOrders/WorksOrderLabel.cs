namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    public class WorksOrderMessage
    {
        public string PartNumber { get; set; }

        public string Message { get; set; }

        public Part Part { get; set; }
    }
}