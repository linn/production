namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PcasRevisionsResponseProcessor : JsonResponseProcessor<IEnumerable<PcasRevision>>
    {
        public PcasRevisionsResponseProcessor(IResourceBuilder<IEnumerable<PcasRevision>> resourceBuilder)
            : base(resourceBuilder, "pcas-revisions", 1)
        {
        }
    }
}