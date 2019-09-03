namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingOperationsModule : NancyModule
    {
        private readonly IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource> manufacturingOperationsService;

        public ManufacturingOperationsModule(IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource> manufacturingOperationsService)
        {
            this.manufacturingOperationsService = manufacturingOperationsService;
            this.Get("/production/resources/manufacturing-operations", _ => this.GetAll());
            this.Get("/production/resources/manufacturing-operations/{manufacturingId*}", parameters => this.GetById(parameters.manufacturingId));
            this.Put("/production/resources/manufacturing-operations/{manufacturingId*}", parameters => this.UpdateManufacturingOperation(parameters.manufacturingId));
            this.Post("/production/resources/manufacturing-operations", parameters => this.AddManufacturingOperation());
        }

        private object GetAll()
        {
            var result = this.manufacturingOperationsService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(int manufacturingId)
        {
            var result = this.manufacturingOperationsService.GetById(manufacturingId);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingOperation(int manufacturingId)
        {
            var resource = this.Bind<ManufacturingOperationResource>();

            var result = this.manufacturingOperationsService.Update(manufacturingId, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingOperation()
        {
            var resource = this.Bind<ManufacturingOperationResource>();

            var result = this.manufacturingOperationsService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
