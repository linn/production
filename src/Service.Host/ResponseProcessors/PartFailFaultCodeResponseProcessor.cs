namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailFaultCodeResponseProcessor : JsonResponseProcessor<PartFailFaultCode>
    {
        public PartFailFaultCodeResponseProcessor(IResourceBuilder<PartFailFaultCode> resourceBuilder)
            : base(resourceBuilder, "part-fail-fault-code", 1)
        {
        }
    }
}