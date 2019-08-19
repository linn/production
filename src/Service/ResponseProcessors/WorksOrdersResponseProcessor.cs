namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class WorksOrdersResponseProcessor : JsonResponseProcessor<IEnumerable<WorksOrder>>
    {
        public WorksOrdersResponseProcessor(IResourceBuilder<IEnumerable<WorksOrder>> resourceBuilder)
            : base(resourceBuilder, "works-orders", 1)
        {
        }
    }
}