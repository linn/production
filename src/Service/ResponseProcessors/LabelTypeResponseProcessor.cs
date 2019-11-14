namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class LabelTypeResponseProcessor : JsonResponseProcessor<LabelType>
    {
        public LabelTypeResponseProcessor(IResourceBuilder<LabelType> resourceBuilder)
            : base(resourceBuilder, "label-type", 1)
        {
        }
    }
}
