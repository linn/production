namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Models;

    public class ShortageSummaryJsonResponseProcessor : JsonResponseProcessor<ShortageSummary>
    {
        public ShortageSummaryJsonResponseProcessor(IResourceBuilder<ShortageSummary> resourceBuilder)
            : base(resourceBuilder, "shortage-summary", 1)
        {
        }
    }
}