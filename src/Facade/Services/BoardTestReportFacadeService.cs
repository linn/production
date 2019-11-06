namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BoardTests;

    public class BoardTestReportFacadeService : IBoardTestReportFacadeService
    {
        private readonly IBoardTestReports boardTestReports;

        public BoardTestReportFacadeService(IBoardTestReports boardTestReports)
        {
            this.boardTestReports = boardTestReports;
        }

        public IResult<ResultsModel> GetBoardTestReport(string fromDate, string toDate, string boardId)
        {
            DateTime.TryParse(fromDate, out var startDate);
            DateTime.TryParse(toDate, out var endDate);
            var result = this.boardTestReports.GetBoardTestReport(startDate, endDate, boardId);
            return new SuccessResult<ResultsModel>(result);
        }

        public IResult<ResultsModel> GetBoardTestDetailsReport(string boardId)
        {
            var result = this.boardTestReports.GetBoardTestDetailsReport(boardId);
            return new SuccessResult<ResultsModel>(result);
        }
    }
}
