﻿namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class IdAndNameListResponseProcessor : JsonResponseProcessor<IEnumerable<IdAndName>>
    {
        public IdAndNameListResponseProcessor(IResourceBuilder<IEnumerable<IdAndName>> resourceBuilder)
            : base(resourceBuilder, "id-and-name-list", 1)
        {
        }
    }
}
