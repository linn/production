namespace Linn.Production.Service.Modules
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class MetalWorkTimingsModule : NancyModule
    {
        private readonly IMetalWorkTimingsService metalWorkTimingsService;

        public MetalWorkTimingsModule(IMetalWorkTimingsService metalWorkTimingsService)
        {
            this.metalWorkTimingsService = metalWorkTimingsService;

            this.Get("/production/reports/mw-timings", _ => this.GetMWTimingsForDates());
            this.Get("/production/reports/mw-timings/export", _ => this.GetMWTimingsExportForDates());

        }

        private object GetMWTimingsForDates()
        {
            var resource = this.Bind<SearchByDatesRequestResource>();
            //todo use new resource to take in two dates
            var worksOrders = this.metalWorkTimingsService.GetMetalWorkTimingsReport(resource.StartDate, resource.EndDate);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetMWTimingsExportForDates()
        {
            var resource = this.Bind<SearchByDatesRequestResource>();
            //todo use new resource to take in two dates
            var worksOrders = this.metalWorkTimingsService.GetMetalWorkTimingsExport(resource.StartDate, resource.EndDate);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }
    }
}
