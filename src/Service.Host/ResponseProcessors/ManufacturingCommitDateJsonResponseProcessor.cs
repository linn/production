namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Models;

    public class ManufacturingCommitDateJsonResponseProcessor : JsonResponseProcessor<ManufacturingCommitDateResults>
    {
        public ManufacturingCommitDateJsonResponseProcessor(IResourceBuilder<ManufacturingCommitDateResults> resourceBuilder)
            : base(resourceBuilder, "manufacturing-commit-date-report", 1)
        {
        }
    }
}