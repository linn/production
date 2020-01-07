namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class BuildPlansReportFacadeService : IBuildPlansReportFacadeService
    {
        private readonly IBuildPlansReportService buildPlansReportService;

        public BuildPlansReportFacadeService(IBuildPlansReportService buildPlansReportService)
        {
            this.buildPlansReportService = buildPlansReportService;
        }

        public IResult<ResultsModel> GetBuildPlansReport(string buildPlanName, int weeks, string citName)
        {
            return new SuccessResult<ResultsModel>(
                this.buildPlansReportService.BuildPlansReport(buildPlanName, weeks, citName));
        }
    }
}