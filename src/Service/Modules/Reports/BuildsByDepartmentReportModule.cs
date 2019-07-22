namespace Linn.Production.Service.Modules.Reports
{
    using System;

    using Domain.LinnApps.Services;

    using Linn.Production.Facade.Services;
    using Linn.Production.Service.Models;

    using Nancy;

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
            var results = this.service.GetBuildsSummary(new DateTime(2006, 1, 27), new DateTime(2006, 1, 28));

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

    }
}