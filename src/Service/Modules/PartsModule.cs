namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
        private readonly IPartsFacadeService partsFacadeService;

        public PartsModule(IPartsFacadeService partsFacadeService)
         {
             this.partsFacadeService = partsFacadeService;
             this.Get("/production/maintenance/parts", _ => this.GetParts());
             this.Get("/production/maintenance/parts/mech-part-source", _ => this.GetMechPartSource());
        }

        private object GetParts()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.partsFacadeService.GetAll()
                              : this.partsFacadeService.SearchParts(resource.SearchTerm);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetMechPartSource()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}