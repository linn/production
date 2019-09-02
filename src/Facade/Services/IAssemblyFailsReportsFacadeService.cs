namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IAssemblyFailsReportsFacadeService
    {
        IResult<ResultsModel> GetAssemblyFailsWaitingListReport();

        IResult<ResultsModel> GetAssemblyFailsMeasuresReport(string fromDate, string toDate);
    }
}