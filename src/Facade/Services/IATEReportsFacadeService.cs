namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IAteReportsFacadeService
    {
        IResult<ResultsModel> GetStatusReport(
            string resourceFromDate,
            string resourceToDate,
            string resourceSmtOrPcb,
            string resourcePlaceFound);

        IResult<ResultsModel> GetDetailsReport(string fromDate, string toDate, string selectBy, string value);
    }
}