namespace Linn.Production.Service.Modules
{
    using System;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ManufacturingRoutesModule : NancyModule
    {
        private readonly IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService;

        private readonly IAuthorisationService AuthorisationService;

        public ManufacturingRoutesModule(IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService, 
                                         IAuthorisationService authorisationService)
        {
            this.manufacturingRouteService = manufacturingRouteService;
            this.AuthorisationService = authorisationService;
            this.Get("/production/resources/manufacturing-routes", _ => this.Search());
            this.Get("/production/resources/manufacturing-routes/{routeCode*}", parameters => this.GetById(parameters.routeCode));
            this.Put("/production/resources/manufacturing-routes/{routeCode*}", parameters => this.UpdateManufacturingRoute(parameters.routeCode));
            this.Post("/production/resources/manufacturing-routes", parameters => this.AddManufacturingRoute());
        }

        private object Search()
        {
            var resource = this.Bind<SearchRequestResource>();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var result = this.manufacturingRouteService.Search(resource.SearchTerm, privileges);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string routeCode)
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var result = this.manufacturingRouteService.GetById(routeCode, privileges);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingRoute(string routeCode)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            if (this.AuthorisationService.HasPermissionFor(AuthorisedAction.ManufacturingRouteUpdate, privileges))
            {
                try
                {
                    var resource = this.Bind<ManufacturingRouteResource>();

                    var result = this.manufacturingRouteService.Update(routeCode, resource, privileges);
                    return this.Negotiate
                        .WithModel(result)
                        .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                        .WithView("Index");
                }
                catch (Exception e)
                {
                    return this.Negotiate.WithModel(new BadRequestResult<Error>(e.Message));
                }
            }
            else
            {
                return HttpStatusCode.Forbidden;
            }
        }

        private object AddManufacturingRoute()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            if (this.AuthorisationService.HasPermissionFor(AuthorisedAction.ManufacturingRouteUpdate, privileges))
            {
                try
                {
                    var resource = this.Bind<ManufacturingRouteResource>();

                    var result = this.manufacturingRouteService.Add(resource, privileges);
                    return this.Negotiate
                        .WithModel(result)
                        .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                        .WithView("Index");
                }
                catch (Exception e)
                {
                    return this.Negotiate.WithModel(new BadRequestResult<Error>(e.Message));
                }
            }
            else
            {
                return HttpStatusCode.Forbidden;
            }
        }
    }
}
