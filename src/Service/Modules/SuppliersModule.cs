namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class SuppliersModule : NancyModule
    {
        private readonly IFacadeService<Supplier, int, SupplierResource, SupplierResource> supplierService;

        public SuppliersModule(IFacadeService<Supplier, int, SupplierResource, SupplierResource> supplierService)
        {
            this.supplierService = supplierService;

            this.Get("/production/maintenance/suppliers", _ => this.GetSuppliers());
        }

        private object GetSuppliers()
        {
            var suppliers = this.supplierService.GetAll();

            return this.Negotiate.WithModel(suppliers).WithMediaRangeModel("/text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}