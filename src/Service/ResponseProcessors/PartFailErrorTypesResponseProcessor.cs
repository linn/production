namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailErrorTypesResponseProcessor : JsonResponseProcessor<IEnumerable<PartFailErrorType>>
    {
        public PartFailErrorTypesResponseProcessor(IResourceBuilder<IEnumerable<PartFailErrorType>> resourceBuilder)
            : base(resourceBuilder, "part-fail-error-types", 1)
        {
        }
    }
}