namespace Linn.Production.Domain.LinnApps.Reports.Smt
{
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class SmtReports : ISmtReports
    {
        private readonly IRepository<WorksOrder, int> worksOrdersRepository;

        private readonly IRepository<BomDetailExplodedPhantomPartView, int> bomDetailRepository;

        private readonly IReportingHelper reportingHelper;

        public SmtReports(
            IRepository<WorksOrder, int> worksOrdersRepository,
            IRepository<BomDetailExplodedPhantomPartView, int> bomDetailRepository,
            IReportingHelper reportingHelper)
        {
            this.worksOrdersRepository = worksOrdersRepository;
            this.bomDetailRepository = bomDetailRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel OutstandingWorksOrderParts(string smtLine, string[] parts)
        {
            var model = new ResultsModel { ReportTitle = new NameModel("Components required for outstanding SMT works orders") };
            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Component") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Board") { SortOrder = 1, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("Qty Required") { SortOrder = 2, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("WO") { SortOrder = 3, GridDisplayType = GridDisplayType.TextValue },
                                  new AxisDetailsModel("WO Qty") { SortOrder = 4, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Qty On Board") { SortOrder = 5, GridDisplayType = GridDisplayType.Value },
                                  new AxisDetailsModel("Line") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue }
                              };
            var workOrders = this.worksOrdersRepository.FilterBy(w => w.Outstanding == "Y" && w.WorkStationCode.StartsWith("SMT"));
            var rowId = 0;
            var values = new List<CalculationValueModel>();
            foreach (var worksOrder in workOrders)
            {
                var bomParts = this.bomDetailRepository.FilterBy(a => a.BomName == worksOrder.PartNumber && a.BomType != "P" && a.DecrementRule != "NO");
                foreach (var bomPart in bomParts)
                {
                    var newRowId = rowId++;
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), TextDisplay = bomPart.PartNumber, ColumnId = "Component" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), TextDisplay = bomPart.BomName, ColumnId = "Board" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), TextDisplay = worksOrder.OrderNumber.ToString(), ColumnId = "WO" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), Quantity = worksOrder.QuantityOutstanding ?? 0, ColumnId = "WO Qty" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), Quantity = bomPart.Quantity, ColumnId = "Qty On Board" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), Quantity = bomPart.Quantity * worksOrder.QuantityOutstanding ?? 0, ColumnId = "Qty Required" });
                    values.Add(new CalculationValueModel { RowId = newRowId.ToString(), TextDisplay = worksOrder.WorkStationCode, ColumnId = "Line" });
                }
            }

            model.AddSortedColumns(columns);
            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);
            this.reportingHelper.SortRowsByTextColumnValues(
                model,
                model.ColumnIndex("Component"),
                model.ColumnIndex("Board"),
                model.ColumnIndex("WO"));
            this.reportingHelper.SubtotalRowsByTextColumnValue(
                model,
                model.ColumnIndex("Component"),
                new[] { model.ColumnIndex("Qty Required") },
                true,
                true);
            return model;
        }
    }
}