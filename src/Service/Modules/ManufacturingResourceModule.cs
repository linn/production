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
        private readonly IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource> ManufacturingResourceService;

        public ManufacturingResourceModule(IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource> ManufacturingResourceService)
        {
            this.ManufacturingResourceService = ManufacturingResourceService;
            this.Get("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.GetManufacturingResourceById(parameters.resourceCode));
            this.Get("/production/resources/manufacturing-resources", _ => this.GetAllManufacturingResources());
            this.Put("/production/resources/manufacturing-resources/{resourceCode*}", parameters => this.UpdateManufacturingResource(parameters.resourceCode));
            this.Post("/production/resources/manufacturing-resources", _ => this.AddManufacturingResource());
        }

        private object GetAllManufacturingResources()
        {
            var result = this.ManufacturingResourceService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetManufacturingResourceById(string faultCode)
        {
            var result = this.ManufacturingResourceService.GetById(faultCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingResource()
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.ManufacturingResourceService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingResource(string resourceCode)
        {
            var resource = this.Bind<ManufacturingResourceResource>();

            var result = this.ManufacturingResourceService.Update(resourceCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
