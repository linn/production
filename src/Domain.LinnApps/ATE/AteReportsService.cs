namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Text.RegularExpressions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Extensions;
    using Linn.Production.Domain.LinnApps.Layouts;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class AteReportsService : IAteReportsService
    {
        private readonly IRepository<AteTest, int> ateTestRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekService linnWeekService;

        private readonly IRepository<Employee, int> employeeRepository;

        public AteReportsService(
            IRepository<AteTest, int> ateTestRepository,
            IReportingHelper reportingHelper,
            ILinnWeekService linnWeekService,
            IRepository<Employee, int> employeeRepository)
        {
            this.ateTestRepository = ateTestRepository;
            this.reportingHelper = reportingHelper;
            this.linnWeekService = linnWeekService;
            this.employeeRepository = employeeRepository;
        }

        public ResultsModel GetStatusReport(
            DateTime fromDate,
            DateTime toDate,
            string smtOrPcb,
            string placeFound,
            AteReportGroupBy groupBy)
        {
            var details = this.GetAteTestDetails(fromDate, toDate, placeFound);

            if (!string.IsNullOrEmpty(smtOrPcb))
            {
                details = details.Where(a => a.SmtOrPcb == smtOrPcb).ToList();
            }

            var reportLayout = new ValuesByWeekLayout(
                this.reportingHelper,
                this.GenerateReportTitle(groupBy),
                null,
                false);
            var weeks = this.linnWeekService.GetWeeks(fromDate, toDate).ToList();
            reportLayout.AddWeeks(
                weeks.Select(
                    w => new AxisDetailsModel(w.LinnWeekNumber.ToString(), w.WeekEndingDDMON)
                             {
                                 SortOrder = w.LinnWeekNumber
                             }));

            reportLayout.AddData(this.CalculateStatusValues(details, groupBy, weeks));

            var model = reportLayout.GetResultsModel();
            this.reportingHelper.SortRowsByRowTitle(model);

            model.RowDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    this.GenerateValueDrillDown(groupBy, fromDate, toDate, smtOrPcb, placeFound)));

            return model;
        }

        public ResultsModel GetDetailsReport(
            DateTime fromDate,
            DateTime toDate,
            string smtOrPcb,
            string placeFound,
            string board,
            string component,
            string faultCode)
        {
            var resultsModel = new ResultsModel
                                   {
                                       ReportTitle = new NameModel(this.GenerateDetailsReportTitle(fromDate, toDate, board, component, faultCode))
                                   };
            resultsModel.AddSortedColumns(
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("Test Id", GridDisplayType.TextValue),
                        new AxisDetailsModel("Board Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("Operator", GridDisplayType.TextValue),
                        new AxisDetailsModel("Item", GridDisplayType.TextValue),
                        new AxisDetailsModel("Batch Number", GridDisplayType.TextValue),
                        new AxisDetailsModel("Circuit Ref", GridDisplayType.TextValue),
                        new AxisDetailsModel("Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("Fault Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("Fails"),
                        new AxisDetailsModel("Smt Or Pcb", GridDisplayType.TextValue),
                        new AxisDetailsModel("DetailOperator", "Operator", GridDisplayType.TextValue)
                    });

            var details = this.GetAteTestDetails(fromDate, toDate, placeFound);
            var reportDetails = this.SelectDetails(details.AsQueryable(), smtOrPcb, board, component, faultCode).ToList();

            this.reportingHelper.AddResultsToModel(
                resultsModel,
                this.CalculateDetailValues(reportDetails),
                CalculationValueModelType.Quantity,
                true);
            resultsModel.RowDrillDownTemplates.Add(
                new DrillDownModel(
                    "Test",
                    "/production/quality/ate-tests/{textValue}",
                    null,
                    resultsModel.ColumnIndex("Test Id")));
            this.reportingHelper.SortRowsByRowTitle(resultsModel);
            this.RemovedRepeatedValues(
                resultsModel,
                resultsModel.ColumnIndex("Test Id"),
                new[] { resultsModel.ColumnIndex("Test Id"), resultsModel.ColumnIndex("Board Part Number") });
            return resultsModel;
        }

        private IEnumerable<AteTestReportDetail> SelectDetails(
            IQueryable<AteTestReportDetail> details,
            string smtOrPcb,
            string board,
            string component,
            string faultCode)
        {
            var expressions = new List<Expression<Func<AteTestReportDetail, bool>>>();

            if (!string.IsNullOrEmpty(smtOrPcb))
            {
                expressions.Add(f => f.SmtOrPcb == smtOrPcb);
            }

            if (!string.IsNullOrEmpty(board))
            {
                expressions.Add(f => f.BoardPartNumber == board);
            }

            if (!string.IsNullOrEmpty(component))
            {
                expressions.Add(f => f.ComponentPartNumber == component);
            }

            if (!string.IsNullOrEmpty(faultCode))
            {
                expressions.Add(f => f.AteTestFaultCode == faultCode);
            }

            return expressions.Aggregate(details, (current, func) => current.Where(func));
        }

        private string GenerateReportTitle(AteReportGroupBy groupBy)
        {
            return $"ATE Test Fails By {Regex.Replace(groupBy.ToString(), "(\\B[A-Z])", " $1")}";
        }

        private string GenerateDetailsReportTitle(DateTime fromDate, DateTime toDate, string board, string component, string faultCode)
        {
            var title = $"ATE Test Fails between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}";
            title += !string.IsNullOrEmpty(board) ? $" for board {board}" : string.Empty;
            title += !string.IsNullOrEmpty(component) ? $" for component {component}" : string.Empty;
            title += !string.IsNullOrEmpty(faultCode) ? $" for fault code {faultCode}" : string.Empty;
            return title;
        }

        private string GenerateValueDrillDown(AteReportGroupBy groupBy, DateTime fromDate, DateTime toDate, string smtOrPcb, string placeFound)
        {
            return $"/production/reports/ate/details/report?{char.ToLowerInvariant(groupBy.ToString()[0]) + groupBy.ToString().Substring(1)}={{rowId}}&parentGroupBy={groupBy.ParseOption()}&placeFound={placeFound}&smtOrPcb={smtOrPcb}&fromDate={WebUtility.UrlEncode(fromDate.ToString("o"))}&toDate={WebUtility.UrlEncode(toDate.ToString("o"))}";
        }

        private IEnumerable<CalculationValueModel> CalculateStatusValues(
            IEnumerable<AteTestReportDetail> details,
            AteReportGroupBy groupBy,
            IReadOnlyCollection<LinnWeek> weeks)
        {
            switch (groupBy)
            {
                case AteReportGroupBy.Component:
                    return details.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.ComponentPartNumber,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTested, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails ?? 0
                                 });
                case AteReportGroupBy.FaultCode:
                    return details.Select(
                        f => new CalculationValueModel
                        {
                            RowId = f.AteTestFaultCode ?? string.Empty,
                            ColumnId = this.linnWeekService.GetWeek(f.DateTested, weeks).LinnWeekNumber.ToString(),
                            Quantity = f.NumberOfFails ?? 0
                        });
                case AteReportGroupBy.Board:
                    return details.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.BoardPartNumber ?? string.Empty,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTested, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails ?? 0
                                 });
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null);
            }
        }

        private IList<CalculationValueModel> CalculateDetailValues(IEnumerable<AteTestReportDetail> details)
        {
            var models = new List<CalculationValueModel>();
            foreach (var ateTestReportDetail in details)
            {
                var rowId = $"{ateTestReportDetail.TestId}/{ateTestReportDetail.ItemNumber:000}";
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Test Id",
                                   TextDisplay = ateTestReportDetail.TestId.ToString()
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Board Part Number",
                                   TextDisplay = ateTestReportDetail.BoardPartNumber
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Operator",
                                   TextDisplay = ateTestReportDetail.PcbOperatorNumber != null ? 
                                                     this.employeeRepository.FindById((int)ateTestReportDetail.PcbOperatorNumber).FullName : null,
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Item",
                                   TextDisplay = ateTestReportDetail.ItemNumber.ToString()
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Batch Number",
                                   TextDisplay = ateTestReportDetail.BatchNumber
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Circuit Ref",
                                   TextDisplay = ateTestReportDetail.CircuitRef
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Part Number",
                                   TextDisplay = ateTestReportDetail.ComponentPartNumber
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Fault Code",
                                   TextDisplay = ateTestReportDetail.AteTestFaultCode
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Fails",
                                   Quantity = ateTestReportDetail.NumberOfFails ?? 0
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "Smt Or Pcb",
                                   TextDisplay = ateTestReportDetail.SmtOrPcb
                               });
                models.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   ColumnId = "DetailOperator",
                                   TextDisplay = ateTestReportDetail.DetailPcbOperator.HasValue
                                                     ? this.employeeRepository.FindById(ateTestReportDetail.DetailPcbOperator.Value).FullName
                                                     : string.Empty
                               });
            }

            return models;
        }

        private IEnumerable<AteTestReportDetail> GetAteTestDetails(DateTime fromDate, DateTime toDate, string placeFound)
        {
            var data = this.ateTestRepository.FilterBy(
                a => a.DateTested != null && a.DateTested.Value.Date >= fromDate.Date
                                          && a.DateTested.Value.Date <= toDate.Date);
            if (!string.IsNullOrEmpty(placeFound))
            {
                data = data.Where(d => d.PlaceFound == placeFound);
            }

            var allDetails = data.ToList().SelectMany(
                a => a.Details,
                (a, detail) => new AteTestReportDetail
                                   {
                                       TestId = a.TestId,
                                       DateTested = a.DateTested ?? throw new ArgumentNullException(),
                                       BoardPartNumber = a.WorksOrder.PartNumber,
                                       NumberTested = a.NumberTested,
                                       ItemNumber = detail.ItemNumber,
                                       SmtOrPcb = detail.SmtOrPcb,
                                       NumberOfFails = detail.NumberOfFails,
                                       AteTestFaultCode = detail.AteTestFaultCode,
                                       BatchNumber = detail.BatchNumber,
                                       CircuitRef = detail.CircuitRef,
                                       ComponentPartNumber = detail.PartNumber,
                                       PcbOperatorNumber = a.PcbOperator?.Id,
                                       DetailPcbOperator = detail.PcbOperator
                                   });

            return allDetails;
        }

        private void RemovedRepeatedValues(ResultsModel model, int columnToCheck, int[] columnsToRemove = null, bool preSorted = true)
        {
            var groups = model.Rows.GroupBy(a => model.GetGridTextValue(a.RowIndex, columnToCheck));
            if (columnsToRemove == null || columnsToRemove.Length == 0)
            {
                columnsToRemove = new[] { columnToCheck };
            }

            if (!preSorted)
            {
                this.reportingHelper.SortRowsByTextColumnValues(model, columnToCheck);
            }

            foreach (var groupedSet in groups)
            {
                var newRowSortOrder = groupedSet.OrderBy(a => a.SortOrder)
                                          .First(a => model.GetGridTextValue(a.RowIndex, columnToCheck) == groupedSet.Key)
                                          .SortOrder ?? 0;

                foreach (var rowModel in groupedSet.Where(g => g.SortOrder != newRowSortOrder))
                {
                    foreach (var columnToRemove in columnsToRemove)
                    {
                        model.SetGridValue(rowModel.RowIndex, columnToRemove, null);
                        model.SetGridTextValue(rowModel.RowIndex, columnToRemove, string.Empty);
                    }
                }
            }
        }
    }
}