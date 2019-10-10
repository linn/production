namespace Linn.Production.Domain.LinnApps.Models
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public class ManufacturingCommitDateResults
    {
        public IEnumerable<ManufacturingCommitDateResult> Results { get; set; }

        public ResultsModel IncompleteLinesAnalysis { get; set; }
    }
}
