namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

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

        IResult<IEnumerable<IEnumerable<string>>> GetPartFailDetailsReportCsv(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department);
    }
}
