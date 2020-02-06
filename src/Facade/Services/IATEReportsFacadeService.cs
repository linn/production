namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IAteReportsFacadeService
    {
        IResult<ResultsModel> GetStatusReport(
            string fromDate,
            string toDate,
            string smtOrPcb,
            string placeFound,
            string groupBy);

        IResult<ResultsModel> GetDetailsReport(
            string fromDate,
            string toDate,
            string smtOrPcb,
            string placeFound,
            string board,
            string component,
            string faultCode);
    }
}