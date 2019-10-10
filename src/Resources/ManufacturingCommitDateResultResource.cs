namespace Linn.Production.Resources
{
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ManufacturingCommitDateResultResource
    {
        public string ProductType { get; set; }

        public int NumberOfLines { get; set; }

        public ReportReturnResource Results { get; set; }
    }
}
