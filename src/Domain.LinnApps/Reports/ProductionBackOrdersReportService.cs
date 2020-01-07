namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class ProductionBackOrdersReportService : IProductionBackOrdersReportService
    {
        private readonly IQueryRepository<ProductionBackOrdersView> productionBackOrdersViewRepository;

        private readonly IRepository<AccountingCompany, string> accountingCompaniesRepository;

        private readonly IRepository<Cit, string> citRepository;

        private readonly IReportingHelper reportingHelper;

        public ProductionBackOrdersReportService(
            IQueryRepository<ProductionBackOrdersView> productionBackOrdersViewRepository,
            IRepository<AccountingCompany, string> accountingCompaniesRepository,
            IRepository<Cit, string> citRepository,
            IReportingHelper reportingHelper)
        {
            this.productionBackOrdersViewRepository = productionBackOrdersViewRepository;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.citRepository = citRepository;
            this.reportingHelper = reportingHelper;
        }

        public IEnumerable<ResultsModel> ProductionBackOrders(string citCode)
        {
            var results = new List<ResultsModel>();

            var accountingCompany = this.accountingCompaniesRepository.FindById("LINN");

            var orders = this.productionBackOrdersViewRepository.FilterBy(a => a.JobId == accountingCompany.LatestSosJobId);
            if (!string.IsNullOrEmpty(citCode))
            {
                orders = orders.Where(a => a.CitCode == citCode);
            }

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Article Number", GridDisplayType.TextValue) { SortOrder = 1, AllowWrap = false },
                                  new AxisDetailsModel("Description", GridDisplayType.TextValue) { SortOrder = 2 },
                                  new AxisDetailsModel("Order Qty", GridDisplayType.Value) { SortOrder = 3 },
                                  new AxisDetailsModel("Order Value", GridDisplayType.Value) { SortOrder = 4 },
                                  new AxisDetailsModel("Oldest Date", GridDisplayType.TextValue) { SortOrder = 5, AllowWrap = false },
                                  new AxisDetailsModel("Can Build Qty", GridDisplayType.Value) { SortOrder = 6 },
                                  new AxisDetailsModel("Can Build Value", GridDisplayType.Value) { SortOrder = 7 }
                              };

            foreach (var cit in orders.OrderBy(a => a.CitCode).GroupBy(g => g.CitCode))
            {
                var citDetails = this.citRepository.FindById(cit.Key);
                var resultsModel = new ResultsModel
                                       {
                                           ReportTitle = new NameModel($"Cit {cit.Key} - {citDetails?.Name}"),
                                           DisplaySequence = citDetails?.SortOrder
                                       };
                resultsModel.AddSortedColumns(columns);

                var values = this.ExtractValues(cit);
                this.reportingHelper.AddResultsToModel(resultsModel, values.ToList(), CalculationValueModelType.Value, true);
                results.Add(resultsModel);
            }

            return results;
        }

        private IEnumerable<CalculationValueModel> ExtractValues(IEnumerable<ProductionBackOrdersView> orders)
        {
            var models = new List<CalculationValueModel>();
            var sortOrder = 0;
            foreach (var productionBackOrdersView in orders.OrderBy(o => o.OldestDate))
            {
                var rowId = sortOrder++;
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Article Number",
                                   TextDisplay = productionBackOrdersView.ArticleNumber,
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Description",
                                   TextDisplay = productionBackOrdersView.InvoiceDescription
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Order Qty",
                                   Value = productionBackOrdersView.OrderQuantity
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Order Value",
                                   Value = productionBackOrdersView.OrderValue
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Oldest Date",
                                   TextDisplay = productionBackOrdersView.OldestDate.ToString("dd-MMM-yyyy")
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Can Build Qty",
                                   Value = productionBackOrdersView.CanBuildQuantity
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId.ToString(),
                                   ColumnId = "Can Build Value",
                                   Value = productionBackOrdersView.CanBuildValue
                               });
            }

            return models;
        }
    }
}