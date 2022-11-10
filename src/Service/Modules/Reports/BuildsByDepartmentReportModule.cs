namespace Linn.Production.Service.Modules.Reports
{
    using System;

    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class BuildsByDepartmentReportModule : NancyModule
    {
        private readonly IBuildsByDepartmentReportFacadeService service;

        public BuildsByDepartmentReportModule(IBuildsByDepartmentReportFacadeService service)
        {
            this.service = service;
            this.Get("/production/reports/builds-summary", _ => this.GetBuildsSummary());
            this.Get("/production/reports/builds-detail", _ => this.GetBuildsDetail());
            this.Get("/production/reports/builds-detail/export", _ => this.GetBuildsDetailExport());
        }

        private object GetBuildsSummary()
        {
            var resource = this.Bind<BuildsSummaryReportOptionsRequestResource>();
            var from = DateTime.Parse(resource.FromDate);
            var to = DateTime.Parse(resource.ToDate);
            var partNumbers = resource.PartNumbers;
            var results = this.service.GetBuildsSummaryReport(from, to, resource.Monthly, partNumbers);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetBuildsDetail()
        {
            var resource = this.Bind<BuildsDetailReportOptionsRequestResource>();
            var from = DateTime.Parse(resource.FromDate);
            var to = DateTime.Parse(resource.ToDate);
            var results = this.service.GetBuildsDetailReport(
                from, to, resource.Department, resource.QuantityOrValue, resource.Monthly, resource.PartNumbers);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetBuildsDetailExport()
        {
            var resource = this.Bind<BuildsDetailReportOptionsRequestResource>();
            var from = DateTime.Parse(resource.FromDate);
            var to = DateTime.Parse(resource.ToDate);
            return this.Negotiate
                .WithModel(this.service.GetBuildsDetailExport(
                    from, to, resource.Department, resource.QuantityOrValue, resource.Monthly))
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }
    }
}                                                                              