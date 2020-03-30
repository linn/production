namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class Part
    {
        public string PartNumber { get; set; }

        public string AccountingCompany { get; set; }

        public int? BomId { get; set; }

        public string BomType { get; set; }

        public string Description { get; set; }

        public string DecrementRule { get; set; }

        public string SernosSequence { get; set; }

        public decimal? BaseUnitPrice { get; set; }

        public int? PreferredSupplier { get; set; }

        public string LibraryRef { get; set; }

        public string LibraryName { get; set; }

        public string FootprintRef { get; set; }

        public List<PartFail> Fails { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }

        public List<WorksOrder> WorksOrders { get; set; }

        public List<BuildPlanDetail> BuildPlanDetails { get; set; }

        public IEnumerable<WorksOrderLabel> WorksOrderLabels { get; set; }

        public bool IsPhantomPart()
        {
            return this.BomType == "P";
        }

        public bool IsBoardPart()
        {
            return this.PartNumber.StartsWith("PCAS") || this.PartNumber.StartsWith("PCSM");
        }
    }
}