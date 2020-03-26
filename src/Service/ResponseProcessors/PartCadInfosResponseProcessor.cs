namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartCadInfosResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<PartCadInfo>>>
    {
        public PartCadInfosResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<PartCadInfo>>> resourceBuilder)
            : base(resourceBuilder, "part-cad-infos-1")
        {
        }
    }
}