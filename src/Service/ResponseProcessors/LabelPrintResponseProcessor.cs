namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class LabelPrintResponseProcessor : JsonResponseProcessor<LabelPrint>
    {
        public LabelPrintResponseProcessor(IResourceBuilder<LabelPrint> resourceBuilder)
            : base(resourceBuilder, "label-print", 1)
        {
        }
    }
}