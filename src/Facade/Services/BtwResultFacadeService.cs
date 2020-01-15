namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class BtwResultFacadeService : IBtwResultFacadeService
    {
        private readonly IBuiltThisWeekReportService builtThisWeekReportService;

        public BtwResultFacadeService(IBuiltThisWeekReportService builtThisWeekReportService)
        {
            this.builtThisWeekReportService = builtThisWeekReportService;
        }

        public IResult<ResultsModel> GenerateBtwResultForCit(string citCode)
        {
            if (string.IsNullOrEmpty(citCode))
            {
                return new BadRequestResult<ResultsModel>("Must specify a cit code");
            }

            return new SuccessResult<ResultsModel>(
                this.builtThisWeekReportService.GetBuiltThisWeekReport(citCode));
        }
    }
}