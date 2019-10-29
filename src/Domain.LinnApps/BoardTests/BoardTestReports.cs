namespace Linn.Production.Domain.LinnApps.BoardTests
{
    using System;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class BoardTestReports : IBoardTestReports
    {
        private readonly IRepository<BoardTest, BoardTestKey> repository;

        public BoardTestReports(IRepository<BoardTest, BoardTestKey> repository)
        {
            this.repository = repository;
        }

        public ResultsModel GetBoardTestReport(DateTime fromDate, DateTime toDate)
        {
            var tests = this.repository.FilterBy(a => a.DateTested >= fromDate && a.DateTested.Date <= toDate).ToList();
            throw new NotImplementedException();
        }
    }
}