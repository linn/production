namespace Linn.Production.Resources
{
    public class WwdDetailResource
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? QtyKitted { get; set; }

        public int? QtyReserved { get; set; }

        public int? QtyAtLocation { get; set; }

        public string StoragePlace { get; set; }

        public string Remarks { get; set; }
    }
}