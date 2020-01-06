namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ATE;

    public class AteTestsResponseProcessor : JsonResponseProcessor<IEnumerable<AteTest>>
    {
        public AteTestsResponseProcessor(IResourceBuilder<IEnumerable<AteTest>> resourceBuilder)
            : base(resourceBuilder, "ate-tests", 1)
        {
        }
    }
}