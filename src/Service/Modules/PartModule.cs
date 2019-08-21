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
        private readonly IFacadeService<Part, string, PartResource, PartResource> partsService;

        public PartsModule(
            IFacadeService<Part, string, PartResource, PartResource> partsService)
        {
            this.partsService = partsService;
            
            this.Get("production/maintenance/parts", _ => this.GetParts());
            
        }

        private object GetParts()
        {
            var resource = this.Bind<SearchRequestResource>();

            var parts = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.partsService.GetAll()
                              : this.partsService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
