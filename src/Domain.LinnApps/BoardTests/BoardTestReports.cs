namespace Linn.Production.Domain.LinnApps.BoardTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class BoardTestReports : IBoardTestReports
    {
        private readonly IRepository<BoardTest, BoardTestKey> repository;

        private readonly IReportingHelper reportingHelper;

        public BoardTestReports(IRepository<BoardTest, BoardTestKey> repository, IReportingHelper reportingHelper)
        {
            this.repository = repository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetBoardTestReport(DateTime fromDate, DateTime toDate, string boardId)
        {
            var results = new ResultsModel { ReportTitle = new NameModel("Board Tests") };
            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Board CitName", GridDisplayType.TextValue) { SortOrder = 0, AllowWrap = false },
                                  new AxisDetailsModel("Board Serial Number", "Board SN", GridDisplayType.TextValue) { SortOrder = 1, AllowWrap = false },
                                  new AxisDetailsModel("First Test Date", "First Test", GridDisplayType.TextValue) { SortOrder = 2, AllowWrap = false },
                                  new AxisDetailsModel("Last Test Date", "Last Test", GridDisplayType.TextValue) { SortOrder = 3, AllowWrap = false },
                                  new AxisDetailsModel("No Of Tests", GridDisplayType.TextValue) { SortOrder = 4 },
                                  new AxisDetailsModel("Passed At Test", GridDisplayType.TextValue) { SortOrder = 5 },
                                  new AxisDetailsModel("Status", GridDisplayType.TextValue) { SortOrder = 6 }
                              };
            results.AddSortedColumns(columns);

            var tests = !string.IsNullOrEmpty(boardId)
                        ? this.repository.FilterBy(
                            t => t.BoardSerialNumber.ToLower().Contains(boardId.ToLower())
                                 && t.DateTested >= fromDate
                                 && t.DateTested.Date <= toDate).ToList()
                        : this.repository.FilterBy(a => a.DateTested >= fromDate && a.DateTested.Date <= toDate).ToList();

            if (tests.Count == 0)
            {
                return results;
            }

            var models = new List<CalculationValueModel>();
            var groupBySn = tests.GroupBy(a => a.BoardSerialNumber);
            foreach (var board in groupBySn)
            {
                var latestBoardName = board.Max(a => a.BoardName);
                var firstPassSeq = board.Any(a => a.Status == "PASS")
                                       ? board.Where(b => b.Status == "PASS").Min(a => a.Seq)
                                       : (int?)null;
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Board CitName", TextDisplay = latestBoardName });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Board Serial Number", TextDisplay = board.Key });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "First Test Date", TextDisplay = board.Min(a => a.DateTested).ToString("dd-MMM-yyyy") });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Last Test Date", TextDisplay = board.Max(a => a.DateTested).ToString("dd-MMM-yyyy") });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "No Of Tests", TextDisplay = board.Count().ToString() });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Passed At Test", TextDisplay = firstPassSeq.ToString() });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Status", TextDisplay = board.First(a => a.Seq == board.Max(b => b.Seq)).Status });
            }

            results.ValueDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    "/production/reports/board-test-details-report?boardId={rowId}",
                    null,
                    results.ColumnIndex("Board Serial Number")));
            this.reportingHelper.AddResultsToModel(results, models, CalculationValueModelType.Quantity, true);
            this.reportingHelper.SortRowsByTextColumnValues(results, 0, 1);
            return results;
        }

        public ResultsModel GetBoardTestDetailsReport(string boardId)
        {
            var results = new ResultsModel { ReportTitle = new NameModel($"Board Test Details for Board Id {boardId}") };
            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Board CitName", GridDisplayType.TextValue) { SortOrder = 0, AllowWrap = false },
                                  new AxisDetailsModel("Board Serial Number", "Board SN", GridDisplayType.TextValue) { SortOrder = 1, AllowWrap = false },
                                  new AxisDetailsModel("Sequence", "Seq", GridDisplayType.TextValue) { SortOrder = 2 },
                                  new AxisDetailsModel("Test Machine", GridDisplayType.TextValue) { SortOrder = 3 },
                                  new AxisDetailsModel("Test Date", "Test Date", GridDisplayType.TextValue) { SortOrder = 4, AllowWrap = false },
                                  new AxisDetailsModel("Time Tested", GridDisplayType.TextValue) { SortOrder = 5, AllowWrap = false },
                                  new AxisDetailsModel("Status", GridDisplayType.TextValue) { SortOrder = 6 },
                                  new AxisDetailsModel("Fail Type", GridDisplayType.TextValue) { SortOrder = 7 }
                              };
            results.AddSortedColumns(columns);

            var tests = this.repository.FilterBy(a => a.BoardSerialNumber.ToLower() == boardId.ToLower()).ToList();
            if (tests.Count == 0)
            {
                return results;
            }

            var models = new List<CalculationValueModel>();
            foreach (var test in tests)
            {
                var rowId = $"{test.BoardSerialNumber}/{test.Seq}";
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Board CitName", TextDisplay = test.BoardName });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Board Serial Number", TextDisplay = test.BoardSerialNumber });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Sequence", TextDisplay = test.Seq.ToString() });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Test Machine", TextDisplay = test.TestMachine });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Test Date", TextDisplay = test.DateTested.ToString("dd-MMM-yyyy") });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Time Tested", TextDisplay = test.TimeTested });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Status", TextDisplay = test.Status });
                models.Add(new CalculationValueModel { RowId = rowId, ColumnId = "Fail Type", TextDisplay = $"{test.FailType?.Type} - {test.FailType?.Description}" });
            }

            this.reportingHelper.AddResultsToModel(results, models, CalculationValueModelType.Quantity, true);
            this.reportingHelper.SortRowsByTextColumnValues(results, results.ColumnIndex("Sequence"));
            return results;
        }
    }
}