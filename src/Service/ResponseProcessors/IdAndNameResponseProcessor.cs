namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class IdAndNameResponseProcessor : JsonResponseProcessor<IdAndName>
    {
        public IdAndNameResponseProcessor(IResourceBuilder<IdAndName> resourceBuilder)
            : base(resourceBuilder, "id-and-name", 1)
        {
        }
    }
}
