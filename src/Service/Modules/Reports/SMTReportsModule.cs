namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SmtReportsModule : NancyModule
    {
        private readonly ISmtReportsFacadeService reportService;

        public SmtReportsModule(ISmtReportsFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/smt/outstanding-works-order-parts/report", _ => this.GetOutstandingWorksOrderParts());
        }

        private object GetOutstandingWorksOrderParts()
        {
            var resource = this.Bind<SmtOutstandingWorksOrderPartsRequestResource>();

            var results = this.reportService.GetPartsForOutstandingWorksOrders(resource.SmtLine, resource.Parts);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}