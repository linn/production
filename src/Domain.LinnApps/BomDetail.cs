namespace Linn.Production.Domain.LinnApps
{
    public class BomDetail
    {
        public int DetailId { get; set; }

        public int BomId { get; set; }

        public Bom Bom { get; set; }

        public string PartNumber { get; set; }

        public Part Part { get; set; }
    }
}
