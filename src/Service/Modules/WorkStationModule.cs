namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WorkStationModule : NancyModule
    {
        private readonly IFacadeService<WorkStation, string, WorkStationResource, WorkStationResource> workStationService;

        public WorkStationModule(
            IFacadeService<WorkStation, string, WorkStationResource, WorkStationResource> workStationService)
        {
            this.workStationService = workStationService;
            this.Get("/production/maintenance/work-stations", _ => this.SearchOrGetAll());
        }

        private object SearchOrGetAll()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = string.IsNullOrEmpty(resource.SearchTerm) ?
                         this.workStationService.GetAll() :
                         this.workStationService.Search(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
