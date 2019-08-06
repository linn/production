namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;

    public class BuildsByDepartmentReportFacadeService : IBuildsByDepartmentReportFacadeService
    {
        private readonly IBuildsSummaryReportService buildsSummaryReportService;
        private readonly IBuildsDetailReportService buildsDetailReportService;

        public BuildsByDepartmentReportFacadeService(
            IBuildsSummaryReportService buildsSummaryReportService,
            IBuildsDetailReportService buildsDetailReportService)
        {
            this.buildsSummaryReportService = buildsSummaryReportService;
            this.buildsDetailReportService = buildsDetailReportService;
        }

        public IResult<IEnumerable<ResultsModel>> GetBuildsSummaryReports(DateTime fromWeek, DateTime toWeek, bool monthly = false)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.buildsSummaryReportService.GetBuildsSummaryReports(fromWeek, toWeek, monthly));
        }

        public IResult<ResultsModel> GetBuildsDetailReport(
            DateTime fromWeek,
            DateTime toWeek,
            string department,
            string quantityOrValue,
            bool monthly = false)
        {
            return new SuccessResult<ResultsModel>(
                this.buildsDetailReportService.GetBuildsDetailReport(
                    fromWeek,
                    toWeek,
                    department,
                    quantityOrValue,
                    monthly));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetBuildsDetailExport(DateTime from, DateTime to, string department, string quantityOrValue, bool monthly)
        {
            var results = this.buildsDetailReportService
                .GetBuildsDetailReport(from, to, department, quantityOrValue, monthly)
                .ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(results);
        }
    }
}