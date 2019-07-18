namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IOutstandingWorksOrdersReportFacade
    {
        IResult<ResultsModel> GetOutstandingWorksOrdersReport();
    }
}
