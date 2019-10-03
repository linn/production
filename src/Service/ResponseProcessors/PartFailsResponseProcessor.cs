namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailsResponseProcessor : JsonResponseProcessor<IEnumerable<PartFail>>
    {
        public PartFailsResponseProcessor(IResourceBuilder<IEnumerable<PartFail>> resourceBuilder)
            : base(resourceBuilder, "part-fails", 1)
        {
        }
    }
}