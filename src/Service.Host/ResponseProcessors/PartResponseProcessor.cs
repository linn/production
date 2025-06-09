namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartResponseProcessor : JsonResponseProcessor<ResponseModel<Part>>
    {
        public PartResponseProcessor(IResourceBuilder<ResponseModel<Part>> resourceBuilder)
            : base(resourceBuilder, "part", 1)
        {
        }
    }
}