namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class ProductionBackOrdersReportService : IProductionBackOrdersReportService
    {
        private readonly IQueryRepository<ProductionBackOrdersView> productionBackOrdersViewRepository;

        private readonly IRepository<AccountingCompany, string> accountingCompaniesRepository;

        private readonly IReportingHelper reportingHelper;

        public ProductionBackOrdersReportService(
            IQueryRepository<ProductionBackOrdersView> productionBackOrdersViewRepository,
            IRepository<AccountingCompany, string> accountingCompaniesRepository,
            IReportingHelper reportingHelper)
        {
            this.productionBackOrdersViewRepository = productionBackOrdersViewRepository;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel ProductionBackOrders(string citCode)
        {
            if (string.IsNullOrEmpty(citCode))
            {
                citCode = "S";
            }

            var accountingCompany = this.accountingCompaniesRepository.FindById("LINN");

            var orders = this.productionBackOrdersViewRepository.FilterBy(a => a.JobId == accountingCompany.LatestSosJobId && a.CitCode == citCode);

            var resultsModel = new ResultsModel { ReportTitle = new NameModel("Production Back Orders") };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Article Number", GridDisplayType.TextValue) { SortOrder = 1 },
                                  new AxisDetailsModel("Description", GridDisplayType.TextValue) { SortOrder = 2 },
                                  new AxisDetailsModel("Order Qty", GridDisplayType.Value) { SortOrder = 3 },
                                  new AxisDetailsModel("Order Value", GridDisplayType.Value) { SortOrder = 4 },
                                  new AxisDetailsModel("Oldest Date", GridDisplayType.TextValue) { SortOrder = 5 },
                                  new AxisDetailsModel("Can Build Qty", GridDisplayType.Value) { SortOrder = 6 },
                                  new AxisDetailsModel("Can Build Value", GridDisplayType.Value) { SortOrder = 7 }
                              };
            resultsModel.AddSortedColumns(columns);

            var values = this.ExtractValues(orders);
            this.reportingHelper.AddResultsToModel(resultsModel, values.ToList(), CalculationValueModelType.Value, true);
            return resultsModel;
        }

        private IEnumerable<CalculationValueModel> ExtractValues(IQueryable<ProductionBackOrdersView> orders)
        {
            var models = new List<CalculationValueModel>();
            foreach (var productionBackOrdersView in orders)
            {
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Article Number",
                                   TextDisplay = productionBackOrdersView.ArticleNumber
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Description",
                                   TextDisplay = productionBackOrdersView.InvoiceDescription
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Order Qty",
                                   Value = productionBackOrdersView.OrderQuantity
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Order Value",
                                   Value = productionBackOrdersView.OrderValue
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Oldest Date",
                                   TextDisplay = productionBackOrdersView.OldestDate.ToString("dd-MMM-yyyy")
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Can Build Qty",
                                   Value = productionBackOrdersView.CanBuildQuantity
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = productionBackOrdersView.ArticleNumber,
                                   ColumnId = "Can Build Value",
                                   Value = productionBackOrdersView.CanBuildValue
                               });
            }

            return models;
        }
    }
}