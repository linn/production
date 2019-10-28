namespace Linn.Production.Domain.LinnApps.ViewModels
{
    public class BomDetailExplodedPhantomPartView
    {
        public string BomName { get; set; }

        public string PartNumber { get; set; }

        public decimal Quantity { get; set; }

        public int BomId { get; set; }

        public string BomType { get; set; }

        public string DecrementRule { get; set; }
    }
}