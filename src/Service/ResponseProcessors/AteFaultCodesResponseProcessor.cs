namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ATE;

    public class AteFaultCodesResponseProcessor : JsonResponseProcessor<IEnumerable<AteFaultCode>>
    {
        public AteFaultCodesResponseProcessor(IResourceBuilder<IEnumerable<AteFaultCode>> resourceBuilder)
            : base(resourceBuilder, "ate-fault-codes", 1)
        {
        }
    }
}