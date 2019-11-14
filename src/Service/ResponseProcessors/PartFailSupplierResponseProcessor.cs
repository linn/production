namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartFailSupplierResponseProcessor : JsonResponseProcessor<PartFailSupplierView>
    {
        public PartFailSupplierResponseProcessor(IResourceBuilder<PartFailSupplierView> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}