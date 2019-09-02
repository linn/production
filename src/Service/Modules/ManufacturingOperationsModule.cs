namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingOperationsModule : NancyModule
    {
        private readonly IManufacturingOperationsFacade manufacturingOperationsService;

        public ManufacturingOperationsModule(IManufacturingOperationsFacade manufacturingOperationsService)
        {
            this.manufacturingOperationsService = manufacturingOperationsService;
            this.Get("/production/resources/manufacturing-operations", _ => this.GetAll());
            this.Get("/production/resources/manufacturing-operations/{routeCode*}/{manufacturingId*}", parameters => this.GetById(parameters.routeCode, parameters.manufacturingId));
            this.Put("/production/resources/manufacturing-operations/{routeCode*}/{manufacturingId*}", parameters => this.UpdateManufacturingOperation(parameters.routeCode, parameters.manufacturingId));
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

        private object GetById(string routeCode, string manufacturingId)
        {
            var result = this.manufacturingOperationsService.GetById(routeCode, int.Parse(manufacturingId));
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingOperation(string routeCode, string manufacturingId)
        {
            var resource = this.Bind<ManufacturingOperationResource>();

            var result = this.manufacturingOperationsService.Update(routeCode, int.Parse(manufacturingId), resource);
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
