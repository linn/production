namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingResourceModule : NancyModule
    {
        private readonly
            IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource> manufacturingResourceService;

        public ManufacturingResourceModule(
            IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource> manufacturingResourceService)
        {
            this.manufacturingResourceService = manufacturingResourceService;
            this.Get("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.GetManufacturingResourceById(parameters.resourceCode));
            this.Get("/production/resources/manufacturing-resources", _ => this.GetAllManufacturingResources());
            this.Put("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.UpdateManufacturingResource(parameters.resourceCode));
            this.Post("/production/resources/manufacturing-resources", _ => this.AddManufacturingResource());
        }

        private object GetAllManufacturingResources()
        {
            var result = this.manufacturingResourceService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetManufacturingResourceById(string faultCode)
        {
            var result = this.manufacturingResourceService.GetById(faultCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingResource()
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.manufacturingResourceService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingResource(string resourceCode)
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.manufacturingResourceService.Update(resourceCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
