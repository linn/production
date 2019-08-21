namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PcasRevisionsModule : NancyModule
    {
        private readonly IFacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource> productionTriggerLevelsService;

        public PcasRevisionsModule(
            IFacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource> productionTriggerLevelsService)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;

            this.Get("production/maintenance/pcas-revisions", _ => this.GetPcasRevisions());
        }

        private object GetPcasRevisions()
        {
            var resource = this.Bind<SearchRequestResource>();

            var parts = string.IsNullOrEmpty(resource.SearchTerm)
                            ? this.productionTriggerLevelsService.GetAll()
                            : this.productionTriggerLevelsService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}