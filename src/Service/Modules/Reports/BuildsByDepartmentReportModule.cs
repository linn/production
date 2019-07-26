namespace Linn.Production.Service.Modules.Reports
{
    using System;

    using Domain.LinnApps.Services;

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
            this.Get("/production/reports/builds-summary", parameters => this.GetBuildsSummary());
        }

        private object GetBuildsSummary()
        {
            var resource = this.Bind<BuildsSummaryReportOptionsRequestResource>();
            var from = DateTime.Parse(resource.FromDate);
            var to = DateTime.Parse(resource.ToDate);
            var results = this.service.GetBuildsSummaryReports(from, to, resource.Monthly);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}                                                                              