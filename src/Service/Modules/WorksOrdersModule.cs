namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Models;

    using Nancy;

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
            return this.Negotiate.WithModel(this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetOutstandingWorksOrdersReportExport()
        {
            return this.Negotiate
                .WithModel(this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv())
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }
    }
}
