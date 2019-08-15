namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class AssemblyFailResponseProcessor : JsonResponseProcessor<AssemblyFail>
    {
        public AssemblyFailResponseProcessor(IResourceBuilder<AssemblyFail> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}
