namespace Linn.Production.Service.Modules
{
    using System;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PurchaseOrdersModule : NancyModule
    {
        private readonly IPurchaseOrderService service;

        private readonly ISernosPack sernosPack;

        public PurchaseOrdersModule(IPurchaseOrderService service, ISernosPack sernosPack)
        {
            this.service = service;
            this.sernosPack = sernosPack;

            this.Get("/production/resources/purchase-orders", _ => this.GetPurchaseOrders());
            this.Get("/production/resources/purchase-orders/{id}", parameters => this.GetPurchaseOrder(parameters.id));
            this.Post("/production/resources/purchase-orders/issue-sernos", _ => this.IssueSernos());
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

        private object IssueSernos()
        {
            var resource = this.Bind<IssueSernosRequestResource>();

            try
            {
                this.sernosPack.IssueSernos(
                    resource.DocumentNumber, 
                    "PO", 
                    resource.DocumentLine,
                    resource.PartNumber, 
                    resource.CreatedBy, 
                    resource.Quantity, 
                    resource.FirstSerialNumber);
            }
            catch (Exception exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }
    }
}
