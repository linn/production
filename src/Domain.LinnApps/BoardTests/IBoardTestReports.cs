namespace Linn.Production.Domain.LinnApps.BoardTests
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IBoardTestReports
    {
        ResultsModel GetBoardTestReport(DateTime fromDate, DateTime toDate, string boardId);
    }
}
