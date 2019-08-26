namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IWhoBuiltWhatReportFacadeService
    {
        IResult<IEnumerable<ResultsModel>> WhoBuiltWhat(string fromDate, string toDate, string citCode);

        IResult<ResultsModel> WhoBuiltWhatDetails(string fromDate, string toDate, int userNumber);
    }
}