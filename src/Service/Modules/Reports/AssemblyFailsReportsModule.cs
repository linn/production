﻿namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AssemblyFailsReportsModule : NancyModule
    {
        private readonly IAssemblyFailsReportsFacadeService reportService;

        public AssemblyFailsReportsModule(IAssemblyFailsReportsFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/assembly-fails-waiting-list", _ => this.GetWaitingListReport());
            this.Get("/production/reports/assembly-fails-measures/report", _ => this.GetMeasuresReport());
            this.Get("/production/reports/assembly-fails-details", _ => this.GetApp());
            this.Get("/production/reports/assembly-fails-details/report/export", _ => this.GetDetailsReportExport());
            this.Get("/production/reports/assembly-fails-details/report", _ => this.GetDetailsReport());
        }

        private object GetDetailsReport()
        {
            var resource = this.Bind<AssemblyFailsDetailsReportRequestResource>();

            var results = this.reportService.GetAssemblyFailsDetailsReport(resource);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetDetailsReportExport()
        {
            var resource = this.Bind<AssemblyFailsDetailsReportRequestResource>();

            var result = this.reportService.GetAssemblyFailsDetailsReportExport(resource);

            return this.Negotiate
                .WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetMeasuresReport()
        {
            var resource = this.Bind<FromToDateGroupByRequestResource>();

            var results = this.reportService.GetAssemblyFailsMeasuresReport(resource.FromDate, resource.ToDate, resource.GroupBy);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetWaitingListReport()
        {
            var results = this.reportService.GetAssemblyFailsWaitingListReport();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
