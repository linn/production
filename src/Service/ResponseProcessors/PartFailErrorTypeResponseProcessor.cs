namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailErrorTypeResponseProcessor : JsonResponseProcessor<PartFailErrorType>
    {
        public PartFailErrorTypeResponseProcessor(IResourceBuilder<PartFailErrorType> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}