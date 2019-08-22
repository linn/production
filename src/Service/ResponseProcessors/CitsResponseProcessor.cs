namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class CitsResponseProcessor : JsonResponseProcessor<IEnumerable<Cit>>
    {
        public CitsResponseProcessor(IResourceBuilder<IEnumerable<Cit>> resourceBuilder)
            : base(resourceBuilder, "cits", 1)
        {
        }
    }
}