namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IAssemblyFailsWaitingListReportFacadeService
    {
        IResult<ResultsModel> GetAssemblyFailsWaitingListReport();
    }
}