namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class LabelReprintResponseProcessor : JsonResponseProcessor<LabelReprint>
    {
        public LabelReprintResponseProcessor(IResourceBuilder<LabelReprint> resourceBuilder)
            : base(resourceBuilder, "label-reprint-reissue", 1)
        {
        }
    }
}