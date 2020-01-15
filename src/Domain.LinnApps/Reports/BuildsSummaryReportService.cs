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

            var model = new ResultsModel { ReportTitle = new NameModel("Builds Summary Report") };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("WeekEnd") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Department") { SortOrder = 1, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Value") { SortOrder = 2, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Days") { SortOrder = 3, GridDisplayType = GridDisplayType.Value, DecimalPlaces = 1 }
                              };

            var rowId = 0;
            model.AddSortedColumns(columns);
            var values = new List<CalculationValueModel>();
            foreach (var summary in summaries)
            {
                values.Add(
                    new CalculationValueModel
                        {    
                            RowId = rowId.ToString(), TextDisplay = summary.WeekEnd.ToShortDateString(), ColumnId = "WeekEnd"
                        });
                values.Add(new CalculationValueModel { RowId = rowId.ToString(), TextDisplay = summary.DepartmentDescription, ColumnId = "Department" });
                values.Add(new CalculationValueModel { RowId = rowId.ToString(), Value = summary.Value, ColumnId = "Value" });
                values.Add(new CalculationValueModel { RowId = rowId.ToString(), Value = summary.DaysToBuild, ColumnId = "Days" });

                model.ValueDrillDownTemplates.Add(
                    new DrillDownModel(
                        "Department",
                        $"/production/reports/builds-detail/options?fromDate={from.Date.ToString("o", CultureInfo.InvariantCulture)}"
                        + $"&toDate={to.Date.ToString("o", CultureInfo.InvariantCulture)}"
                        + $"&department={summaries[rowId].DepartmentCode}&quantityOrValue=Value" + $"&monthly={monthly}",
                        rowId,
                        model.ColumnIndex("Department")));
                rowId++;
            }

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Value, true);
            
            this.reportingHelper.SubtotalRowsByTextColumnValue(
                model,
                model.ColumnIndex("WeekEnd"),
                new[] { model.ColumnIndex("Value"), model.ColumnIndex("Days") },
                true,
                true);

            return model;
        }
    }
}