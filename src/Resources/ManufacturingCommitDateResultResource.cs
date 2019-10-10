namespace Linn.Production.Resources
{
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ManufacturingCommitDateResultResource
    {
        public string ProductType { get; set; }

        public int NumberOfLines { get; set; }

        public int NumberSupplied { get; set; }

        public decimal PercentageSupplied { get; set; }

        public int NumberAvailable { get; set; }

        public decimal PercentageAvailable { get; set; }

        public ReportReturnResource Results { get; set; }
    }
}
