namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources.RequestResources;

    public interface IAssemblyFailsReportsFacadeService
    {
        IResult<ResultsModel> GetAssemblyFailsWaitingListReport();

        IResult<ResultsModel> GetAssemblyFailsMeasuresReport(string fromDate, string toDate, string groupBy);

        IResult<ResultsModel> GetAssemblyFailsDetailsReport(AssemblyFailsDetailsReportRequestResource resource);
    }
}