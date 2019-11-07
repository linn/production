namespace Linn.Production.Domain.LinnApps
{
    public class WwdDetail
    {
        public int WwdJobId { get; set; }

        public string PtlJobref { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? QtyKitted { get; set; }

        public int? QtyReserved { get; set; }

        public int? QtyAtLocation { get; set; }

        public string StoragePlace { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public string Remarks { get; set; }
    }
}