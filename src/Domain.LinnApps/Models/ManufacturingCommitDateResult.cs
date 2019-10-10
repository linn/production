namespace Linn.Production.Domain.LinnApps.Models
{
    using Linn.Common.Reporting.Models;

    public class ManufacturingCommitDateResult
    {
        public string ProductType { get; set; }

        public int NumberOfLines { get; set; }

        public ResultsModel Results { get; set; }
    }
}
