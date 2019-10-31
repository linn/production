namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IBoardTestReportFacadeService
    {
        IResult<ResultsModel> GetBoardTestReport(string fromDate, string toDate, string boardId);
    }
}