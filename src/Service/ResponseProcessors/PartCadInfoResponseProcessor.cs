namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartCadInfoResponseProcessor : JsonResponseProcessor<PartCadInfo>
    {
        public PartCadInfoResponseProcessor(IResourceBuilder<PartCadInfo> resourceBuilder)
            : base(resourceBuilder, "part-cad-info", 1)
        {
        }
    }
}