namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PurchaseOrdersModule : NancyModule
    {
        private readonly IPurchaseOrderService service;

        public PurchaseOrdersModule(IPurchaseOrderService service)
        {
            this.service = service;

            this.Get("/production/resources/purchase-orders", _ => this.GetPurchaseOrders());
            this.Get("/production/resources/purchase-order/{id}", parameters => this.GetPurchaseOrder(parameters.id));
        }

        private object GetPurchaseOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var purchaseOrders = this.service.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(purchaseOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetPurchaseOrder(int id)
        {
            var purchaseOrder = this.service.GetPurchaseOrderWithSernosInfo(id);

            return this.Negotiate.WithModel(purchaseOrder).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
