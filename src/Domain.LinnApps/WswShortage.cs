namespace Linn.Production.Domain.LinnApps
{
    public class WswShortage
    {
        public string Jobref { get; set; }

        public string CitCode { get; set; }

        public string PartNumber { get; set; }

        public string ShortPartNumber { get; set; }

        public string ShortPartDescription { get; set; }

        public string ShortageCategory { get; set; }

        public int Required { get; set; }

        public int Stock { get; set; }

        public int AdjustedAvailable { get; set; }

        public int QtyReserved { get; set; }

        public int? KittingPriority { get; set; }

        public int? CanBuild { get; set; }

        public bool IsBoardShortage()
        {
            return ShortageCategory == "EP";
        }

        public bool IsProcurementShortage()
        {
            return ShortageCategory == "PROC";
        }

        public bool IsMetalworkShortage()
        {
            return ShortageCategory == "CP";
        }
    }
}
