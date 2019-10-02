namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Exceptions;

    public class ErrorResponseProcessor : JsonResponseProcessor<Error>
    {
        public ErrorResponseProcessor(IResourceBuilder<Error> resourceBuilder)
            : base(resourceBuilder, "error", 1)
        {
        }
    }
}