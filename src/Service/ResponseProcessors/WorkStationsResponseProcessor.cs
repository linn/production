namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class WorkStationsResponseProcessor : JsonResponseProcessor<IEnumerable<WorkStation>>
    {
        public WorkStationsResponseProcessor(IResourceBuilder<IEnumerable<WorkStation>> resourceBuilder)
            : base(resourceBuilder, "work-stations", 1)
        {
        }
    }
}
