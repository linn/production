namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingResourceModule : NancyModule
    {
        private readonly IFacadeFilterService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource, ManufacturingResourceResource> manufacturingResourceFacadeService;

        public ManufacturingResourceModule(IFacadeFilterService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource, ManufacturingResourceResource> manufacturingResourceFacadeService)
        {
            this.manufacturingResourceFacadeService = manufacturingResourceFacadeService;
            this.Get("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.GetManufacturingResourceById(parameters.resourceCode));
            this.Get("/production/resources/manufacturing-resources/all", _ => this.GetAllManufacturingResources());
            this.Get("/production/resources/manufacturing-resources", _ => this.GetManufacturingResources());
            this.Put("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.UpdateManufacturingResource(parameters.resourceCode));
            this.Post("/production/resources/manufacturing-resources", _ => this.AddManufacturingResource());
        }

        private object GetAllManufacturingResources()
        {
            var result = this.manufacturingResourceFacadeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetManufacturingResources()
        {
            var resource = this.Bind<ManufacturingResourceResource>();
            var result = this.manufacturingResourceFacadeService.FilterBy(resource);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetManufacturingResourceById(string faultCode)
        {
            var result = this.manufacturingResourceFacadeService.GetById(faultCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingResource()
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.manufacturingResourceFacadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingResource(string resourceCode)
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.manufacturingResourceFacadeService.Update(resourceCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
