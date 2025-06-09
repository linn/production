namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailFaultCodesResponseProcessor : JsonResponseProcessor<IEnumerable<AssemblyFailFaultCode>>
    {
        public AssemblyFailFaultCodesResponseProcessor(IResourceBuilder<IEnumerable<AssemblyFailFaultCode>> resourceBuilder)
            : base(resourceBuilder, "assembly-fail-fault-codes", 1)
        {
        }
    }
}