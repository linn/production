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
        private readonly IBuildsByDepartmentReportService buildsByDepartmentReportService;

        public BuildsByDepartmentReportFacadeService(
            IBuildsByDepartmentReportService buildsByDepartmentReportService)
        {
            this.buildsByDepartmentReportService = buildsByDepartmentReportService;
        }

        public IResult<IEnumerable<ResultsModel>> GetBuildsSummaryReports(DateTime fromWeek, DateTime toWeek, bool monthly = false)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.buildsByDepartmentReportService.GetBuildsSummaryReports(fromWeek, toWeek, monthly));
        }
    }
}