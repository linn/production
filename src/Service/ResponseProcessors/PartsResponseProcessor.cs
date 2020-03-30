namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartsResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<Part>>>
    {
        public PartsResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<Part>>> resourceBuilder)
            : base(resourceBuilder, "parts", 1)
        {
        }
    }
}