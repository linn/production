namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class BuildsSummaryReportService : IBuildsSummaryReportService
    {
        private readonly IBuildsSummaryReportDatabaseService databaseService;

        public BuildsSummaryReportService(
            IBuildsSummaryReportDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public IEnumerable<ResultsModel> GetBuildsSummaryReports(DateTime from, DateTime to, bool monthly = false)
        {
            var summaries = this.databaseService.GetBuildsSummaries(from, to, monthly).ToList();

            var weeks = summaries.GroupBy(s => s.WeekEnd.Date);
            var reports = new List<ResultsModel>();
            foreach (var week in weeks)
            {
                var results = new ResultsModel(new[]
                                                   {
                                                       "Dept", "Value", "Days"
                                                   })
                                  {
                                      ReportTitle = new NameModel(
                                          week.Key.ToString("d", CultureInfo.CurrentCulture))
                                  };

                foreach (var summary in week.OrderBy(w => w.DepartmentDescription))
                {
                    results.SetColumnType(0, GridDisplayType.TextValue);
                    results.SetColumnType(1, GridDisplayType.Value);
                    results.SetColumnType(2, GridDisplayType.Value);

                    var row = results.AddRow(summary.DepartmentCode);
                    results.SetGridTextValue(row.RowIndex, 0, summary.DepartmentDescription);
                    results.SetGridValue(row.RowIndex, 1, summary.Value, decimalPlaces: 1);
                    results.SetGridValue(row.RowIndex, 2, summary.DaysToBuild, decimalPlaces: 1);
                }

                reports.Add(results);
            }

            reports.Add(this.GetDepartmentTotals(summaries, from, to, monthly));
            return reports;
        }

        private ResultsModel GetDepartmentTotals(IEnumerable<BuildsSummary> summaries, DateTime from, DateTime to, bool monthly)
        {
            var results = new ResultsModel(new[]
                                              {
                                                  "Department", "Total Value", "Total Days"
                                              })
                             {
                                 ReportTitle = new NameModel("Totals")
                             };

            var departments = summaries.GroupBy(a => new { Code = a.DepartmentCode, Description = a.DepartmentDescription }).Select(
                a => new { a.Key, TotalValue = a.Sum(b => b.Value), TotalDays = a.Sum(b => b.DaysToBuild) });

            foreach (var department in departments)
            {
                results.SetColumnType(0, GridDisplayType.TextValue);
                results.SetColumnType(1, GridDisplayType.Value);
                results.SetColumnType(2, GridDisplayType.Value);

                var row = results.AddRow(department.Key.Code);
                results.SetGridTextValue(row.RowIndex, 0, department.Key.Description);
                results.SetGridValue(row.RowIndex, 1, department.TotalValue, decimalPlaces: 1);
                results.SetGridValue(row.RowIndex, 2, department.TotalDays, decimalPlaces: 1);
            }

            results.RowDrillDownTemplates.Add(
                new DrillDownModel(
                    "department",
                    $"/production/reports/builds-detail/options?fromDate={from.Date.ToString("o", CultureInfo.InvariantCulture)}&toDate={to.Date.ToString("o", CultureInfo.InvariantCulture)}" + "&department={rowId}"
                                                                                                 + $"&quantityOrValue=Value&monthly={monthly}"));
            return results;
        }
    }
}