namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PartResponseProcessor : JsonResponseProcessor<Part>
    {
        public PartResponseProcessor(IResourceBuilder<Part> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}
