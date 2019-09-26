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

        private readonly IReportingHelper reportingHelper;

        public BuildsSummaryReportService(
            IBuildsSummaryReportDatabaseService databaseService, IReportingHelper reportingHelper)
        {
            this.databaseService = databaseService;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetBuildsSummaryReports(DateTime from, DateTime to, bool monthly = false)
        {
            var summaries = this.databaseService.GetBuildsSummaries(from, to, monthly).ToList();

            var interval = monthly ? "Month" : "Week";

            var weeks = summaries.GroupBy(s => s.WeekEnd.Date);

            var model = new ResultsModel { ReportTitle = new NameModel("Builds Summary Report") };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("WeekEnd") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Department") { SortOrder = 1, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Value") { SortOrder = 2, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Days") { SortOrder = 3, GridDisplayType = GridDisplayType.Value }
                              };

            var rowId = 0;

            var values = new List<CalculationValueModel>();
            foreach (var summary in summaries)
            {
                var newRowId = summary.DepartmentCode.ToString();

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId, TextDisplay = summary.WeekEnd.ToShortDateString(), ColumnId = "WeekEnd"
                        });
                values.Add(new CalculationValueModel { RowId = newRowId, TextDisplay = summary.DepartmentDescription, ColumnId = "Department" });
                values.Add(new CalculationValueModel { RowId = newRowId, Value = summary.Value, ColumnId = "Value" });
                values.Add(new CalculationValueModel { RowId = newRowId, Value = summary.DaysToBuild, ColumnId = "Days" });
            }

            model.AddSortedColumns(columns);
            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Value, true); // correct? sort by dates??
            this.reportingHelper.SubtotalRowsByTextColumnValue(
                model,
                model.ColumnIndex("WeekEnd"),
                new[] { model.ColumnIndex("Value"), model.ColumnIndex("Days") },
                true,
                true);

            var department = string.Empty;

            model.ValueDrillDownTemplates.Add(
                new DrillDownModel(
                    "Department",
                    $"/production/reports/builds-detail/options?fromDate={from.Date.ToString("o", CultureInfo.InvariantCulture)}"
                    + $"&toDate={to.Date.ToString("o", CultureInfo.InvariantCulture)}"
                    + "&department={rowId}&quantityOrValue=Value" + $"&monthly={monthly}",
                    null,
                    model.ColumnIndex("Department")));
            
            return model;
        }
    }
}