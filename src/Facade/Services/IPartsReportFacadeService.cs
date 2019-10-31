namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IPartsReportFacadeService
    {
        IResult<ResultsModel> GetPartFailDetailsReport(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department);
    }
}
