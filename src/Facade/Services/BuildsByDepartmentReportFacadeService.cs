namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Domain.LinnApps;
    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.Services;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public class BuildsByDepartmentReportFacadeService : IBuildsByDepartmentReportFacadeService
    {
        private readonly IBuildsSummaryReportService buildsSummaryReportService;

        public BuildsByDepartmentReportFacadeService(
            IBuildsSummaryReportService buildsSummaryReportService)
        {
            this.buildsSummaryReportService = buildsSummaryReportService;
        }

        public IResult<IEnumerable<ResultsModel>> GetBuildsSummaryReports(DateTime fromWeek, DateTime toWeek, bool monthly = false)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.buildsSummaryReportService.GetBuildsSummaryReports(fromWeek, toWeek, monthly));
        }
    }
}