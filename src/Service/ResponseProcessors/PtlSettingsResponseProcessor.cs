namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class PtlSettingsResponseProcessor : JsonResponseProcessor<ResponseModel<PtlSettings>>
    {
        public PtlSettingsResponseProcessor(IResourceBuilder<ResponseModel<PtlSettings>> resourceBuilder)
            : base(resourceBuilder, "ptl-settings", 1)
        {
        }
    }
}