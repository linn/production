namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class LabelTypesResponseProcessor : JsonResponseProcessor<IEnumerable<LabelType>>
    {
        public LabelTypesResponseProcessor(IResourceBuilder<IEnumerable<LabelType>> resourceBuilder)
            : base(resourceBuilder, "label-types", 1)
        {
        }
    }
}
