namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources;

    public interface IOutstandingWorksOrdersReportFacade
    {
        IResult<ResultsModel> GetOutstandingWorksOrdersReport(string reportType, string searchParameter);

        IResult<IEnumerable<IEnumerable<string>>> GetOutstandingWorksOrdersReportCsv(string reportType, string searchParameter);
    }
}
