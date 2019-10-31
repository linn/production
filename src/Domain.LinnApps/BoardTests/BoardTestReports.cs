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
                                  new AxisDetailsModel("Board Name", GridDisplayType.TextValue) { SortOrder = 0, AllowWrap = false },
                                  new AxisDetailsModel("Board Serial Number", "Board SN", GridDisplayType.TextValue) { SortOrder = 1, AllowWrap = false },
                                  new AxisDetailsModel("First Test Date", "First Test", GridDisplayType.TextValue) { SortOrder = 2, AllowWrap = false },
                                  new AxisDetailsModel("Last Test Date", "Last Test", GridDisplayType.TextValue) { SortOrder = 3, AllowWrap = false },
                                  new AxisDetailsModel("No Of Tests", GridDisplayType.TextValue) { SortOrder = 4 },
                                  new AxisDetailsModel("Passed At Test", GridDisplayType.TextValue) { SortOrder = 5 },
                                  new AxisDetailsModel("Status", GridDisplayType.TextValue) { SortOrder = 6 }
                              };
            results.AddSortedColumns(columns);

            var tests = this.repository.FilterBy(a => a.DateTested >= fromDate && a.DateTested.Date <= toDate).ToList();
            if (!string.IsNullOrEmpty(boardId))
            {
                tests = tests.Where(t => t.BoardSerialNumber.ToLower().Contains(boardId.ToLower())).ToList();
            }

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
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Board Name", TextDisplay = latestBoardName });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Board Serial Number", TextDisplay = board.Key });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "First Test Date", TextDisplay = board.Min(a => a.DateTested).ToString("dd-MMM-yyyy") });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Last Test Date", TextDisplay = board.Max(a => a.DateTested).ToString("dd-MMM-yyyy") });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "No Of Tests", TextDisplay = board.Count().ToString() });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Passed At Test", TextDisplay = firstPassSeq.ToString() });
                models.Add(new CalculationValueModel { RowId = board.Key, ColumnId = "Status", TextDisplay = board.First(a => a.Seq == board.Max(b => b.Seq)).Status });
            }

            this.reportingHelper.AddResultsToModel(results, models, CalculationValueModelType.Quantity, true);
            this.reportingHelper.SortRowsByTextColumnValues(results, 0, 1);
            return results;
        }
    }
}