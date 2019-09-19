namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartsResponseProcessor : JsonResponseProcessor<IEnumerable<Part>>
    {
        public PartsResponseProcessor(IResourceBuilder<IEnumerable<Part>> resourceBuilder)
            : base(resourceBuilder, "parts", 1)
        {
        }
    }
}