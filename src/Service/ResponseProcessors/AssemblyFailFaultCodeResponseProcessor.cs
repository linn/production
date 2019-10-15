namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailFaultCodeResponseProcessor : JsonResponseProcessor<AssemblyFailFaultCode>
    {
        public AssemblyFailFaultCodeResponseProcessor(IResourceBuilder<AssemblyFailFaultCode> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}