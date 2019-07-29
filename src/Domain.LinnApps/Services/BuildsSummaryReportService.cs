using Linn.Production.Domain.LinnApps.Reports;

namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Linn.Common.Reporting.Models;


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
            var summaries = this.databaseService.GetBuildsSummaries(from, to, monthly).ToList(); // enumerate here
            
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

                foreach (var summary in week.OrderBy(w => w.Department))
                {
                    results.SetColumnType(0, GridDisplayType.TextValue);
                    results.SetColumnType(1, GridDisplayType.Value);
                    results.SetColumnType(2, GridDisplayType.Value);

                    var row = results.AddRow(summary.Department);
                    results.SetGridTextValue(row.RowIndex, 0, summary.Department); // is this a code??
                    results.SetGridValue(row.RowIndex, 1, summary.Value, decimalPlaces: 1);
                    results.SetGridValue(row.RowIndex, 2, summary.DaysToBuild, decimalPlaces: 1);
                }

                reports.Add(results);
            }
            reports.Add(this.GetDepartmentTotals(summaries));
            return reports;
        }

        private ResultsModel GetDepartmentTotals(IEnumerable<BuildsSummary> summaries)
        {
            var results = new ResultsModel(new[]
                                              {
                                                  "Department", "Total Value", "Total Days"
                                              })
                             {
                                 ReportTitle = new NameModel("Totals")
                             };

            var departments = summaries.GroupBy(a => a.Department).Select(
                a => new { a.Key, TotalValue = a.Sum(b => b.Value), TotalDays = a.Sum(b => b.DaysToBuild) });

            foreach (var department in departments)
            {
                results.SetColumnType(0, GridDisplayType.TextValue);
                results.SetColumnType(1, GridDisplayType.Value);
                results.SetColumnType(2, GridDisplayType.Value);

                var row = results.AddRow(department.Key);
                results.SetGridTextValue(row.RowIndex, 0, department.Key); // what is key??
                results.SetGridValue(row.RowIndex, 1, department.TotalValue, decimalPlaces: 1);
                results.SetGridValue(row.RowIndex, 2, department.TotalDays, decimalPlaces: 1);
            }
            return results;
        }
    }
}