namespace Linn.Production.Resources
{
    public class ProductionBackOrderResource
    {
        public int JobId { get; set; }

        public string CitCode { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string ArticleNumber { get; set; }

        public decimal BackOrderQty { get; set; }

        public decimal BaseValue { get; set; }

        public string RequestedDeliveryDate { get; set; }

        public string DatePossible { get; set; }

        public int QueuePosition { get; set; }
    }
}