namespace Linn.Production.Domain.LinnApps.SalesOrders
{
    using System;

    public class SalesOrderDetails
    {
        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string ArticleNumber { get; set; }

        public DateTime RequestedDeliveryDate { get; set; }

        public DateTime FirstAdvisedDespatchDate { get; set; }
    }
}