namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AssemblyFailsReportsModule : NancyModule
    {
        private readonly IAssemblyFailsReportsFacadeService reportService;

        public AssemblyFailsReportsModule(IAssemblyFailsReportsFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/assembly-fails-waiting-list", _ => this.GetReport());
            this.Get("/production/reports/assembly-fails-measures", _ => this.GetMeasuresReport());
        }

        private object GetMeasuresReport()
        {
            var resource = this.Bind<FromToDateRequestResource>();

            var results = this.reportService.GetAssemblyFailsMeasuresReport(resource.FromDate, resource.ToDate);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
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