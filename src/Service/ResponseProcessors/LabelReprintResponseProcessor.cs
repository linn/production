namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class LabelReprintResponseProcessor : JsonResponseProcessor<ResponseModel<LabelReprint>>
    {
        public LabelReprintResponseProcessor(IResourceBuilder<ResponseModel<LabelReprint>> resourceBuilder)
            : base(resourceBuilder, "label-reprint-reissue", 1)
        {
        }
    }
}