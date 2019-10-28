namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ManufacturingCommitDateResultsResource
    {
        public IEnumerable<ManufacturingCommitDateResultResource> Results { get; set; }

        public ManufacturingCommitDateResultResource Totals { get; set; }

        public ReportReturnResource IncompleteLinesAnalysis { get; set; }
    }
}
