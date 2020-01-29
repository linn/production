namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class FailsReportService : IFailsReportService
    {
        private readonly IQueryRepository<FailedParts> failedPartsRepository;

        private readonly IReportingHelper reportingHelper;

        public FailsReportService(IQueryRepository<FailedParts> failedPartsRepository, IReportingHelper reportingHelper)
        {
            this.failedPartsRepository = failedPartsRepository;
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
                                  new AxisDetailsModel("Part Number", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Description", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Qty"),
                                  new AxisDetailsModel("Total Value"),
                                  new AxisDetailsModel("Date Booked", GridDisplayType.TextValue),
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
                        this.ExtractData(rowId.ToString(), fail),
                        CalculationValueModelType.Value,
                        true);
                    rowId++;
                }

                results.Add(result);
            }


            return results;
        }

        private IList<CalculationValueModel> ExtractData(string rowId, FailedParts fail)
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
    }
}