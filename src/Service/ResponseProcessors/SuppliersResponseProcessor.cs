namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class SuppliersResponseProcessor : JsonResponseProcessor<IEnumerable<Supplier>>
    {
        public SuppliersResponseProcessor(IResourceBuilder<IEnumerable<Supplier>> resourceBuilder)
            : base(resourceBuilder, "supplier", 1)
        {
        }
    }
}