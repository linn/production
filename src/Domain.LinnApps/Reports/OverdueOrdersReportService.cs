namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

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
            int jobId,
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod)
        {
            var linn = this.accountingCompaniesRepository.FindById("LINN");

            var data = this.overdueOrderLineQueryRepository.FilterBy(o => o.JobId == linn.LatestSosJobId);

            var model = new ResultsModel { ReportTitle = new NameModel("Outstanding Sales Orders by Days Late") };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Customer Name") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Order") { SortOrder = 1, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Article Number") { SortOrder = 2, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Description") { SortOrder = 3, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Requested Date") { SortOrder = 4, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Days Late") { SortOrder = 5, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("First Advised") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Days Late 2") { SortOrder = 7, GridDisplayType = GridDisplayType.Value },
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
                            RowId = newRowId.ToString(), TextDisplay = row.OutletName, ColumnId = "Customer Name"
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
                            TextDisplay = row.RequestedDeliveryDate.ToString(),
                            ColumnId = "Requested Date"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), Quantity = row.DaysLate, ColumnId = "Days Late"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(),
                            TextDisplay = row.FirstAdvisedDespatchDate.ToString(),
                            ColumnId = "First Advised"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = newRowId.ToString(), Quantity = row.DaysLateFa, ColumnId = "Days Late 2"
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