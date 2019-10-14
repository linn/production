namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class OrdersReports : IOrdersReports
    {
        private readonly IQueryRepository<MCDLine> mcdRepository;

        private readonly IReportingHelper reportingHelper;

        public OrdersReports(IQueryRepository<MCDLine> mcdRepository, IReportingHelper reportingHelper)
        {
            this.mcdRepository = mcdRepository;
            this.reportingHelper = reportingHelper;
        }

        public ManufacturingCommitDateResults ManufacturingCommitDate(string date)
        {
            var reportDate = DateTime.Parse(date).Date;
            var returnResults = new ManufacturingCommitDateResults();
            var rawData = this.mcdRepository.FilterBy(a => a.MCDDate == reportDate);

            returnResults.Results = this.CalculateResultsGroupedByCoreType(rawData);
            returnResults.IncompleteLinesAnalysis = new ResultsModel();

            return returnResults;
        }

        private IEnumerable<ManufacturingCommitDateResult> CalculateResultsGroupedByCoreType(IQueryable<MCDLine> data)
        {
            var coreTypeResults = new List<ManufacturingCommitDateResult>();

            foreach (var coreType in data.GroupBy(a => a.CoreType))
            {
                var numberOfLines = coreType.Count();
                var numberSupplied = coreType.Sum(a => a.OrderLineCompleted);
                var numberAvailable = coreType.Select(b => b.Invoiced + b.CouldGo >= b.QtyOrdered ? 1 : 0).Sum();
                coreTypeResults.Add(
                    new ManufacturingCommitDateResult
                        {
                            NumberOfLines = numberOfLines,
                            NumberSupplied = numberSupplied,
                            PercentageSupplied = this.GetPercentage(numberSupplied, numberOfLines),
                            NumberAvailable = numberAvailable,
                            PercentageAvailable = this.GetPercentage(numberAvailable, numberOfLines),
                            ProductType = coreType.Key,
                            Results = this.BuildDetailsResultsModel(coreType)
                    });
            }

            return coreTypeResults;
        }

        private ResultsModel BuildDetailsResultsModel(IEnumerable<MCDLine> coreType)
        {
            var model = new ResultsModel();
            model.AddSortedColumns(this.GetResultColumns());
            var values = new List<CalculationValueModel>();
            foreach (var mcdLine in coreType)
            {
                this.ExtractValues(mcdLine, values);
            }

            this.reportingHelper.AddResultsToModel(
                model,
                values.OrderBy(a => a.RowId).ToList(),
                CalculationValueModelType.Quantity,
                true);
            return model;
        }

        private List<AxisDetailsModel> GetResultColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Order Number", GridDisplayType.TextValue),
                           new AxisDetailsModel("Order Line", GridDisplayType.TextValue),
                           new AxisDetailsModel("Date Raised", GridDisplayType.TextValue),
                           new AxisDetailsModel("Date Requested", GridDisplayType.TextValue),
                           new AxisDetailsModel("Article Number", GridDisplayType.TextValue),
                           new AxisDetailsModel("Qty Ordered", GridDisplayType.Value),
                           new AxisDetailsModel("Qty Invoiced", GridDisplayType.Value),
                           new AxisDetailsModel("Qty Could Go", GridDisplayType.Value),
                           new AxisDetailsModel("Status", GridDisplayType.TextValue),
                           new AxisDetailsModel("Reason", GridDisplayType.TextValue)
                       };
        }

        private void ExtractValues(MCDLine mcdLine, ICollection<CalculationValueModel> values)
        {
            var rowId = $"{mcdLine.OrderNumber}/{mcdLine.OrderLine}";
            values.Add(
                new CalculationValueModel
                    {
                        RowId = rowId, ColumnId = "Order Number", TextDisplay = mcdLine.OrderNumber.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = rowId, ColumnId = "Order Line", TextDisplay = mcdLine.OrderLine.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = rowId, ColumnId = "Date Raised", TextDisplay = mcdLine.OrderDate.ToString("dd-MMM-yy")
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = rowId, ColumnId = "Date Requested", TextDisplay = mcdLine.RequestedDeliveryDate.ToString("dd-MMM-yy")
                    });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Article Number", TextDisplay = mcdLine.ArticleNumber });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Status", TextDisplay = mcdLine.Status });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Qty Ordered", Quantity = mcdLine.QtyOrdered });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Qty Invoiced", Quantity = mcdLine.Invoiced });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Qty Could Go", Quantity = mcdLine.CouldGo });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Status", TextDisplay = mcdLine.Status });
            values.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Reason", TextDisplay = mcdLine.Reason });
        }

        private decimal GetPercentage(int value, int total)
        {
            return total == 0 ? 0 : decimal.Round(decimal.Divide(value, total) * 100, 1);
        }
    }
}