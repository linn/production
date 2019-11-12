namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartFailSuppliersResponseProcessor : JsonResponseProcessor<IEnumerable<PartFailSupplierView>>
    {
        public PartFailSuppliersResponseProcessor(IResourceBuilder<IEnumerable<PartFailSupplierView>> resourceBuilder)
            : base(resourceBuilder, "part-fail-suppliers", 1)
        {
        }
    }
}