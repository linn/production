namespace Domain.LinnApps.Services
{
    using System;
    using System.Globalization;

    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.Repositories;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class BuildsByDepartmentReportService : IBuildsByDepartmentReportService
    {
        private readonly ILrpPack lrpPack;

        private readonly IBuildsRepository buildsRepository;

        private readonly IRepository<Department, string> departmentRepository;

        public BuildsByDepartmentReportService(IBuildsRepository buildsRepository, ILrpPack lrpPack, IRepository<Department, string> departmentRepository)
        {
            this.lrpPack = lrpPack;
            this.buildsRepository = buildsRepository;
            this.departmentRepository = departmentRepository;
        }

        public ResultsModel GetBuildsSummaryReport(DateTime from, DateTime to)
        {
            var summaries = this.buildsRepository.GetBuildsByDepartment(from, to);

            var results = new ResultsModel(new[] { "Department", "Weekend", "Value", "Days" })
                              {
                                  ReportTitle = new NameModel("Report")
                              };

            results.SetColumnType(0, GridDisplayType.TextValue);
            results.SetColumnType(1, GridDisplayType.TextValue);
            results.SetColumnType(2, GridDisplayType.TextValue);
            results.SetColumnType(3, GridDisplayType.TextValue);

            var rowId = 0;
            foreach (var summary in summaries)
            {
                var row = results.AddRow(rowId.ToString());
                results.SetGridTextValue(rowId, 0, this.departmentRepository.FindById(summary.Department).Description);
                results.SetGridTextValue(rowId, 1, summary.WeekEnd.ToString("d", CultureInfo.CurrentCulture));
                results.SetGridTextValue(rowId, 2, summary.Value.ToString("0.0"));
                results.SetGridTextValue(rowId, 3, summary.DaysToBuild.ToString("0.0"));
                rowId++;
            }

            return results;
        }
    }
}