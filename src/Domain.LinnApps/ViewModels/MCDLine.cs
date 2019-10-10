namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class MCDLine
    {
        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequestedDeliveryDate { get; set; }

        public string CoreType { get; set; }

        public string ArticleNumber { get; set; }

        public int QtyAllocated { get; set; }

        public int QtyOrdered { get; set; }

        public int QtyOutstanding { get; set; }

        public int Invoiced { get; set; }

        public DateTime MCDDate { get; set; }

        public string Status { get; set; }

        public int OrderLineCompleted { get; set; }

        public string Reason { get; set; }

        public int CouldGo { get; set; }
    }
}
