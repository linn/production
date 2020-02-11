namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class ProductionMeasuresReportService : IProductionMeasuresReportService
    {
        private readonly IQueryRepository<FailedParts> failedPartsRepository;

        private readonly IQueryRepository<ProductionDaysRequired> productionDaysRequiredRepository;

        private readonly IReportingHelper reportingHelper;

        public ProductionMeasuresReportService(
            IQueryRepository<FailedParts> failedPartsRepository,
            IQueryRepository<ProductionDaysRequired> productionDaysRequiredRepository,
            IReportingHelper reportingHelper)
        {
            this.failedPartsRepository = failedPartsRepository;
            this.productionDaysRequiredRepository = productionDaysRequiredRepository;
            this.reportingHelper = reportingHelper;
        }

        public IEnumerable<ResultsModel> FailedPartsReport(string citCode)
        {
            var results = new List<ResultsModel>();
            var fails = this.failedPartsRepository.FindAll();
            if (!string.IsNullOrEmpty(citCode))
            {
                fails = fails.Where(a => a.CitCode == citCode);
            }

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                                  new AxisDetailsModel("Description", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Qty"),
                                  new AxisDetailsModel("Total Value") { DecimalPlaces = 2 },
                                  new AxisDetailsModel("Date Booked", GridDisplayType.TextValue) { AllowWrap = false },
                                  new AxisDetailsModel("User Name", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Storage Place", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Supplier Id", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Supplier Name", GridDisplayType.TextValue)
                              };

            var citFails = fails.GroupBy(a => a.CitCode);
            foreach (var citFailGroup in citFails)
            {
                var rowId = 0;
                var result = new ResultsModel { ReportTitle = new NameModel(citFailGroup.FirstOrDefault()?.CitName) };
                result.AddSortedColumns(columns);
                foreach (var fail in citFailGroup.OrderBy(g => g.PartNumber))
                {
                    this.reportingHelper.AddResultsToModel(
                        result,
                        this.ExtractFailData(rowId.ToString(), fail),
                        CalculationValueModelType.Value,
                        true);
                    rowId++;
                }

                results.Add(result);
            }

            return results;
        }

        public IEnumerable<ResultsModel> DayRequiredReport(string citCode)
        {
            var results = new List<ResultsModel>();
            var daysRequired = this.productionDaysRequiredRepository.FindAll();
            if (!string.IsNullOrEmpty(citCode))
            {
                daysRequired = daysRequired.Where(a => a.CitCode == citCode);
            }

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                                  new AxisDetailsModel("Description", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Qty Being Built"),
                                  new AxisDetailsModel("Build"),
                                  new AxisDetailsModel("Can Build"),
                                  new AxisDetailsModel("Can Build Ex PCSM"),
                                  new AxisDetailsModel("Effective Kanban"),
                                  new AxisDetailsModel("Being Built Days", GridDisplayType.TextValue) { AllowWrap = false },
                                  new AxisDetailsModel("Can Build Days", GridDisplayType.TextValue) { AllowWrap = false }
                              };

            var priorityGroup = daysRequired
                .OrderBy(a => a.Priority)
                .GroupBy(a => a.Priority);
            foreach (var priorityGrouping in priorityGroup)
            {
                var rowId = 0;
                var result = new ResultsModel { ReportTitle = new NameModel($"Priority {priorityGrouping.FirstOrDefault()?.Priority}") };
                result.AddSortedColumns(columns);
                foreach (var productionDaysRequired in priorityGrouping
                    .OrderBy(g => g.SortOrder)
                    .ThenBy(a => a.EarliestRequestedDate)
                    .ThenBy(b => b.PartNumber))
                {
                    this.reportingHelper.AddResultsToModel(
                        result,
                        this.ExtractDaysRequiredData(rowId.ToString(), productionDaysRequired),
                        CalculationValueModelType.Value,
                        true);
                    rowId++;
                }

                this.reportingHelper.AddResultsToModel(
                    result,
                    this.ExtractDaysRequiredDataTotals(rowId.ToString(), priorityGrouping),
                    CalculationValueModelType.Value,
                    true);

                results.Add(result);
            }

            return results;
        }

        private IList<CalculationValueModel> ExtractFailData(string rowId, FailedParts fail)
        {
            var models = new List<CalculationValueModel>
                             {
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Part Number", TextDisplay = fail.PartNumber },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Description", TextDisplay = fail.PartDescription },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Qty", Value = fail.Qty },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Total Value", Value = fail.TotalValue },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Date Booked", TextDisplay = fail.DateBooked.ToString("dd-MMM-yyyy") },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "User Name", TextDisplay = fail.CreatedBy },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Storage Place", TextDisplay = fail.StoragePlace },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Supplier Id", TextDisplay = fail.PreferredSupplierId?.ToString() },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Supplier Name", TextDisplay = fail.SupplierName }
                             };

            return models;
        }

        private IList<CalculationValueModel> ExtractDaysRequiredData(string rowId, ProductionDaysRequired daysRequired)
        {
            var models = new List<CalculationValueModel>
                             {
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Part Number", TextDisplay = daysRequired.PartNumber },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Description", TextDisplay = daysRequired.PartDescription },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Qty Being Built", Value = daysRequired.QtyBeingBuilt },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Build", Value = daysRequired.BuildQty },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build", Value = daysRequired.CanBuild },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build Ex PCSM", Value = daysRequired.CanBuildExcludingSubAssemblies },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Effective Kanban", Value = daysRequired.EffectiveKanbanSize },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Being Built Days", TextDisplay = this.GetDaysDisplay(daysRequired.QtyBeingBuiltDays) },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build Days", TextDisplay = this.GetDaysDisplay(daysRequired.CanBuildDays) }
                             };

            return models;
        }

        private IList<CalculationValueModel> ExtractDaysRequiredDataTotals(string rowId, IGrouping<string, ProductionDaysRequired> priorityGrouping)
        {
            var totalBeingBuilt = priorityGrouping.Sum(a => a.QtyBeingBuilt);
            var totalBuild = priorityGrouping.Sum(a => a.BuildQty);
            var totalCanBuild = priorityGrouping.Sum(a => a.CanBuild);
            var totalCanBuildExSubAssemblies = priorityGrouping.Sum(a => a.CanBuildExcludingSubAssemblies);
            var totalBeingBuiltDays = priorityGrouping.Sum(a => a.QtyBeingBuiltDays);
            var totalCanBuildDays = priorityGrouping.Sum(a => a.CanBuildDays);

            var models = new List<CalculationValueModel>
                             {
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Part Number", TextDisplay = "Totals" },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Qty Being Built", Value = totalBeingBuilt },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Build", Value = totalBuild },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build", Value = totalCanBuild },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build Ex PCSM", Value = totalCanBuildExSubAssemblies },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Being Built Days", TextDisplay = this.GetDaysDisplay(totalBeingBuiltDays) },
                                 new CalculationValueModel { RowId = rowId, ColumnId = "Can Build Days", TextDisplay = this.GetDaysDisplay(totalCanBuildDays) }
                             };

            return models;
        }

        private string GetDaysDisplay(double daysValue)
        {
            const int WorkingMinutesPerDay = 410;

            var days = (int)Math.Floor(daysValue);
            var hours = (int)Math.Floor((daysValue - days) * WorkingMinutesPerDay / 60);
            var minutes = (int)((daysValue - days) * WorkingMinutesPerDay) - (hours * 60);

            return $"{days}d {hours}h {minutes}m";
        }
    }
}