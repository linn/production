namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PurchaseOrdersModule : NancyModule
    {
        private readonly IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> service;

        public PurchaseOrdersModule(IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> service)
        {
            this.service = service;

            this.Get("/production/resources/purchase-orders", _ => this.GetPurchaseOrders());
        }

        private object GetPurchaseOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var worksOrders = this.service.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
