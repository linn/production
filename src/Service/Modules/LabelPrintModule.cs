namespace Linn.Production.Service.Modules
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LabelPrintModule : NancyModule
    {
        private readonly ILabelPrintService labelPrintService;
        private readonly IFacadeWithSearchReturnTen<Address, int, AddressResource, AddressResource> addressService;
        private readonly IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource> supplierService;


        public LabelPrintModule(
            ILabelPrintService labelPrintService,
            IFacadeWithSearchReturnTen<Address, int, AddressResource, AddressResource> addressService,
            IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource> supplierService)
        {
            this.labelPrintService = labelPrintService;
            this.addressService = addressService;
            this.supplierService = supplierService;

            this.Get("production/maintenance/labels/print", _ => this.GetApp());
            this.Get("production/maintenance/labels/suppliers", _ => this.SearchSuppliers());
            this.Get("production/maintenance/labels/addresses", _ => this.SearchAddresses());
            this.Post("production/maintenance/labels/print", _ => this.Print());
            this.Get("production/maintenance/labels/printers", _ => this.GetPrinters());
            this.Get("production/maintenance/labels/label-types", _ => this.GetLabelsTypes());
        }

        private object GetPrinters()
        {
            var result = this.labelPrintService.GetPrinters();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetLabelsTypes()
        {
            return this.Negotiate.WithModel(this.labelPrintService.GetLabelTypes())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object SearchSuppliers()
        {
            var resource = this.Bind<SearchRequestResource>();
           
            var result = this.supplierService.SearchReturnTen(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object SearchAddresses()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = this.addressService.SearchReturnTen(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Print()
        {
            var resource = this.Bind<LabelPrintResource>();

            var result = this.labelPrintService.PrintLabel(resource);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
       
        private object GetApp()
        {
            return this.Negotiate
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
