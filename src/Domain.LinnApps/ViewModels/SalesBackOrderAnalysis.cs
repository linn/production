namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class SalesBackOrderAnalysis
    {
        public int JobId { get; set; }

        public int BaseValue { get; set; }

        public int Quantity { get; set; }

        public DateTime RequestedDeliveryDate { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int VAllocated { get; set; }

        public int QAllocated { get; set; }

        public int VRnSNd { get; set; }

        public int QRnSNd { get; set; }

        public int QRnNs { get; set; }

        public int VRnNs { get; set; }

        public int VRtmS { get; set; }

        public int QRtmS { get; set; }

        public int VRtmNs { get; set; }

        public int QRtmNs { get; set; }

        public int VRnmS { get; set; }

        public int QRnmS { get; set; }

        public int VRnmNs { get; set; }

        public int QRnmNs { get; set; }

        public string RnSNdReasons { get; set; }
    }
}
