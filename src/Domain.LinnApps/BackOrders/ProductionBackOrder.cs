namespace Linn.Production.Domain.LinnApps.BackOrders
{
    using System;

    public class ProductionBackOrder
    {
        public int JobId { get; set; }

        public string CitCode { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string ArticleNumber { get; set; }

        public decimal BackOrderQty { get; set; }

        public decimal BaseValue { get; set; }

        public DateTime? RequestedDeliveryDate { get; set; }

        public DateTime? DatePossible { get; set; }

        public int QueuePosition { get; set; }

        public DateTime? ProductionDate { get; set; }
    }
}
