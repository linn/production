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
            returnResults.Totals = this.CalculateTotals(rawData);
            returnResults.IncompleteLinesAnalysis = this.IncompleteLinesAnalysis(rawData);

            return returnResults;
        }

        private IEnumerable<ManufacturingCommitDateResult> CalculateResultsGroupedByCoreType(IQueryable<MCDLine> data)
        {
            var coreTypeResults = new List<ManufacturingCommitDateResult>();

            foreach (var coreType in data.GroupBy(a => a.CoreType).OrderBy(a => string.IsNullOrEmpty(a.Key) ? "XXX" : a.Key))
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

        private ManufacturingCommitDateResult CalculateTotals(IQueryable<MCDLine> data)
        {
            var numberOfLines = data.Count();
            var numberSupplied = data.Sum(a => a.OrderLineCompleted);
            var numberAvailable = data.Select(b => b.Invoiced + b.CouldGo >= b.QtyOrdered ? 1 : 0).Sum();
            return new ManufacturingCommitDateResult
                        {
                            NumberOfLines = numberOfLines,
                            NumberSupplied = numberSupplied,
                            PercentageSupplied = this.GetPercentage(numberSupplied, numberOfLines),
                            NumberAvailable = numberAvailable,
                            PercentageAvailable = this.GetPercentage(numberAvailable, numberOfLines),
                            ProductType = "Totals"
            };
        }

        private ResultsModel IncompleteLinesAnalysis(IEnumerable<MCDLine> data)
        {
            var model = new ResultsModel { ReportTitle = new NameModel("Incomplete Lines Analysis") };
            var notSuppliedLines = data.Where(mcdLine => !string.IsNullOrEmpty(mcdLine.Reason)).ToList();
            var totalNotSupplied = notSuppliedLines.Count;
            var rows = new List<AxisDetailsModel>
                           {
                               new AxisDetailsModel("Allocated", GridDisplayType.Value),
                               new AxisDetailsModel("No Stock", GridDisplayType.Value),
                               new AxisDetailsModel("Credit Limit", GridDisplayType.Value),
                               new AxisDetailsModel("Supply In Full", GridDisplayType.Value),
                               new AxisDetailsModel("Account Hold", GridDisplayType.Value),
                               new AxisDetailsModel("Shipment Hold", GridDisplayType.Value),
                               new AxisDetailsModel("Dunno", GridDisplayType.Value)
                           };
            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Qty", GridDisplayType.Value),
                                  new AxisDetailsModel("%", GridDisplayType.Value)
                              };
            model.AddSortedRows(rows);
            model.AddSortedColumns(columns);
            this.reportingHelper.ZeroPad(model);

            if (totalNotSupplied > 0)
            {
                var allocated = notSuppliedLines.Count(a => a.Reason.Contains("allocated"));
                var noStock = notSuppliedLines.Count(a => a.Reason.Contains("No Stock"));
                var creditLimit = notSuppliedLines.Count(a => a.Reason.Contains("CR"));
                var supplyInFull = notSuppliedLines.Count(a => a.Reason.Contains("SIF"));
                var accountHold = notSuppliedLines.Count(a => a.Reason.Contains("AH"));
                var shipmentHold = notSuppliedLines.Count(a => a.Reason.Contains("SH"));
                var dunno = totalNotSupplied - (noStock + creditLimit + supplyInFull + accountHold + shipmentHold);
                model.SetGridValue(model.RowIndex("Allocated"), model.ColumnIndex("Qty"), allocated);
                model.SetGridValue(model.RowIndex("Allocated"), model.ColumnIndex("%"), this.GetPercentage(allocated, totalNotSupplied));
                model.SetGridValue(model.RowIndex("No Stock"), model.ColumnIndex("Qty"), noStock);
                model.SetGridValue(model.RowIndex("No Stock"), model.ColumnIndex("%"), this.GetPercentage(noStock, totalNotSupplied));
                model.SetGridValue(model.RowIndex("Credit Limit"), model.ColumnIndex("Qty"), creditLimit);
                model.SetGridValue(model.RowIndex("Credit Limit"), model.ColumnIndex("%"), this.GetPercentage(creditLimit, totalNotSupplied));
                model.SetGridValue(model.RowIndex("Supply In Full"), model.ColumnIndex("Qty"), supplyInFull);
                model.SetGridValue(model.RowIndex("Supply In Full"), model.ColumnIndex("%"), this.GetPercentage(supplyInFull, totalNotSupplied));
                model.SetGridValue(model.RowIndex("Account Hold"), model.ColumnIndex("Qty"),  accountHold);
                model.SetGridValue(model.RowIndex("Account Hold"), model.ColumnIndex("%"), this.GetPercentage(accountHold, totalNotSupplied));
                model.SetGridValue(model.RowIndex("Shipment Hold"), model.ColumnIndex("Qty"), shipmentHold);
                model.SetGridValue(model.RowIndex("Shipment Hold"), model.ColumnIndex("%"), this.GetPercentage(shipmentHold, totalNotSupplied));
                model.SetGridValue(model.RowIndex("Dunno"), model.ColumnIndex("Qty"), dunno);
                model.SetGridValue(model.RowIndex("Dunno"), model.ColumnIndex("%"), this.GetPercentage(dunno, totalNotSupplied));
            }

            return model;
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