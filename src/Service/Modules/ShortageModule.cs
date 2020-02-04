namespace Linn.Production.Service.Modules
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ShortageModule : NancyModule
    {
        private readonly IShortageSummaryFacadeService shortageSummaryFacadeService;

        public ShortageModule(IShortageSummaryFacadeService shortageSummaryFacadeService)
        {
            this.shortageSummaryFacadeService = shortageSummaryFacadeService;
            this.Get("/production/reports/shortages", _ => this.GetShortages());
        }

        private object GetShortages()
        {
            var resource = this.Bind<ShortageRequestResource>();

            var results = this.shortageSummaryFacadeService.ShortageSummaryByCit(
                resource.CitCode, resource.PtlJobref);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}