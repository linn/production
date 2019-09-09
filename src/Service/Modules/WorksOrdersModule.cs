namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WorksOrdersModule : NancyModule
    {
        private readonly IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade;

        private readonly IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource> worksOrdersService;

        public WorksOrdersModule(
            IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade,
            IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource> worksOrdersService)
        {
            this.worksOrdersService = worksOrdersService;
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;
            this.Get("production/maintenance/works-orders", _ => this.GetWorksOrders());
            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());
        }

        private object GetOutstandingWorksOrdersReport()
        {
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport(resource.ReportType, resource.SearchParameter);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetOutstandingWorksOrdersReportExport()
        {
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv(resource.ReportType, resource.SearchParameter);

            return this.Negotiate
                .WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetWorksOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var worksOrders = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.worksOrdersService.GetAll()
                              : this.worksOrdersService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
