namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources;

    public interface IOutstandingWorksOrdersReportFacade
    {
        IResult<ResultsModel> GetOutstandingWorksOrdersReport(OutstandingWorksOrdersRequestResource options);

        IResult<IEnumerable<IEnumerable<string>>> GetOutstandingWorksOrdersReportCsv(OutstandingWorksOrdersRequestResource options);
    }
}
