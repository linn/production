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

        private readonly ILrpPack lrpPack;

        private readonly ILinnWeekPack weekPack;

        public BuildsByDepartmentReportFacadeService(
            IBuildsByDepartmentReportService buildsByDepartmentReportService,
            ILrpPack lrpPack,
            ILinnWeekPack weekPack )
        {
            this.buildsByDepartmentReportService = buildsByDepartmentReportService;
            this.lrpPack = lrpPack;
            this.weekPack = weekPack;
        }

        public IResult<ResultsModel> GetBuildsSummary(DateTime fromWeek, DateTime toWeek)
        {
            return new SuccessResult<ResultsModel>(this.buildsByDepartmentReportService.GetBuildsSummaryReport(fromWeek, toWeek));
        }
    }
}