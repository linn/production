namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class OverdueOrdersReportService : IOverdueOrdersService
    {
        private readonly IQueryRepository<OverdueOrderLine> overdueOrderLineQueryRepository;

        private readonly IRepository<AccountingCompany, string> accountingCompaniesRepository;

        private readonly IReportingHelper reportingHelper;

        public OverdueOrdersReportService(
            IQueryRepository<OverdueOrderLine> overdueOrderLineQueryRepository,
            IReportingHelper reportingHelper,
            IRepository<AccountingCompany, string> accountingCompaniesRepository)
        {
            this.overdueOrderLineQueryRepository = overdueOrderLineQueryRepository;
            this.reportingHelper = reportingHelper;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
        }

        public ResultsModel OverdueOrdersReport(
            string reportBy,
            string daysMethod)
        {
            var linn = this.accountingCompaniesRepository.FindById("LINN");

            var data = reportBy == "First Advised Date"
                       ? this.overdueOrderLineQueryRepository.FilterBy(o => o.JobId == linn.LatestSosJobId)
                           .OrderBy(d => d.FirstAdvisedDespatchDate)
                       : this.overdueOrderLineQueryRepository.FilterBy(o => o.JobId == linn.LatestSosJobId);

            var model = new ResultsModel { ReportTitle = new NameModel("Outstanding Sales Orders by Days Late") };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Customer CitName") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Order") { SortOrder = 1, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Article Number") { SortOrder = 2, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Description") { SortOrder = 3, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Requested Date") { SortOrder = 4, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Requested Days Late") { SortOrder = 5, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("First Advised") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Advised Days Late") { SortOrder = 7, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Quantity") { SortOrder = 8, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Value") { SortOrder = 9, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Total Order Value") { SortOrder = 10, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Story") { SortOrder = 11, GridDisplayType = GridDisplayType.TextValue }
                              };

            model.AddSortedColumns(columns);

            var values = new List<CalculationValueModel>();
            var rowId = 0;
            foreach (var row in data)
            {
                var newRowId = rowId++;

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), TextDisplay = row.OutletName, ColumnId = "Customer CitName"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), TextDisplay = row.OrderRef, ColumnId = "Order"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), TextDisplay = row.ArticleNumber, ColumnId = "Article Number"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), TextDisplay = row.InvoiceDescription, ColumnId = "Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            TextDisplay = row.RequestedDeliveryDate?.ToString("dd-MMM-yy"),
                            ColumnId = "Requested Date"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            Quantity = daysMethod == "Working Days" ? row.WorkingDaysLate : row.DaysLate,
                            ColumnId = "Requested Days Late"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            TextDisplay = row.FirstAdvisedDespatchDate?.ToString("dd-MMM-yy"),
                            ColumnId = "First Advised"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            Quantity = daysMethod == "Working Days" ? row.WorkingDaysLateFa : row.DaysLateFa,
                            ColumnId = "Advised Days Late"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            Quantity = row.Quantity, ColumnId = "Quantity"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            Quantity = row.BaseValue, ColumnId = "Value"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            Quantity = row.OrderValue, ColumnId = "Total Order Value"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), TextDisplay = row.Reasons, ColumnId = "Story"
                        });
            }

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }
    }
}