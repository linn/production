namespace Linn.Production.Service.Modules
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Facade.ResourceBuilders;
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
            var purchaseOrder = this.service.GetById(id);
            if (purchaseOrder is SuccessResult<PurchaseOrder>)
            {
                var model = ((SuccessResult<PurchaseOrder>)purchaseOrder).Data;
                var resource = new PurchaseOrderResourceBuilder().Build(model);
                resource.Details.ForEach(d =>
                    {
                        d.FirstSernos = this.service.GetFirstSernos(resource.OrderNumber);
                        d.LastSernos = this.service.GetLastSernos(resource.OrderNumber);
                        d.SernosIssued = this.service.GetSernosIssued(resource.OrderNumber);
                        d.SernosBuilt = this.service.GetSernosBuilt(
                            resource.OrderNumber,
                            d.PartNumber,
                            d.FirstSernos,
                            d.LastSernos);
                    });

                return new SuccessResult<PurchaseOrderResource>(resource);
            }

            return this.Negotiate.WithModel(purchaseOrder).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
