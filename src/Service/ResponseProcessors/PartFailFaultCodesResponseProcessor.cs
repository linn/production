namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailFaultCodesResponseProcessor : JsonResponseProcessor<IEnumerable<PartFailFaultCode>>
    {
        public PartFailFaultCodesResponseProcessor(IResourceBuilder<IEnumerable<PartFailFaultCode>> resourceBuilder)
            : base(resourceBuilder, "part-fail-fault-codes", 1)
        {
        }
    }
}