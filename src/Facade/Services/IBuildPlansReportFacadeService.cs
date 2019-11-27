namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IBuildPlansReportFacadeService
    {
        IResult<ResultsModel> GetBuildPlansReport(string buildPlanName, int weeks, string citName);
    }
}