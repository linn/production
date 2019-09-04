namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingRoutesModule : NancyModule
    {
        private readonly IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService;

        public ManufacturingRoutesModule(IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService)
        {
            this.manufacturingRouteService = manufacturingRouteService;
            this.Get("/production/resources/manufacturing-routes", _ => this.GetAll());
            this.Get("/production/resources/manufacturing-routes/{routeCode*}", parameters => this.GetById(parameters.routeCode));
            this.Put("/production/resources/manufacturing-routes/{routeCode*}", parameters => this.UpdateManufacturingRoute(parameters.routeCode));
            this.Post("/production/resources/manufacturing-routes", parameters => this.AddManufacturingRoute());
        }

        private object GetAll()
        {
            var result = this.manufacturingRouteService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string routeCode)
        {
            var result = this.manufacturingRouteService.GetById(routeCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingRoute(string routeCode)
        {
            var resource = this.Bind<ManufacturingRouteResource>();

            var result = this.manufacturingRouteService.Update(routeCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingRoute()
        {
            var resource = this.Bind<ManufacturingRouteResource>();

            var result = this.manufacturingRouteService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
