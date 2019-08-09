namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WorksOrdersModule : NancyModule
    {
        private readonly IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade;

        public WorksOrdersModule(IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade)
        {
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;

            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());
        }

        private object GetOutstandingWorksOrdersReport()
        {
            // TODO tests with options
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport(resource);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetOutstandingWorksOrdersReportExport()
        {
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv(resource);

            return this.Negotiate
                .WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }
    }
}
