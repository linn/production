namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IOutstandingWorksOrdersReportFacade
    {
        IResult<ResultsModel> GetOutstandingWorksOrdersReport(string reportType, string searchParameter);

        IResult<IEnumerable<IEnumerable<string>>> GetOutstandingWorksOrdersReportCsv(string reportType, string searchParameter);
    }
}
