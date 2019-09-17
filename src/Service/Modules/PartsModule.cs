namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
         private readonly IFacadeService<Part, string, PartResource, PartResource> partFacadeService;

         public PartsModule(IFacadeService<Part, string, PartResource, PartResource> partFacadeService)
         {
             this.partFacadeService = partFacadeService;
             this.Get("/production/maintenance/parts", _ => this.GetParts());
         }

         private object GetParts()
         {
             var resource = this.Bind<SearchRequestResource>();
             var results = string.IsNullOrEmpty(resource.SearchTerm)
                                   ? this.partFacadeService.GetAll()
                                   : this.partFacadeService.Search(resource.SearchTerm);
             return this.Negotiate
                 .WithModel(results)
                 .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                 .WithView("Index");
        }
    }
}