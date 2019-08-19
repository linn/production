namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class AssemblyFailsWaitingListReportModule : NancyModule
    {
        private readonly IAssemblyFailsWaitingListReportFacadeService reportService;

        public AssemblyFailsWaitingListReportModule(IAssemblyFailsWaitingListReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/assembly-fails-waiting-list", _ => this.GetReport());
        }

        private object GetReport()
        {
            var results = this.reportService.GetAssemblyFailsWaitingListReport();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}