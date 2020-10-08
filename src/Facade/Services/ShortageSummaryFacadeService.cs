namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class ShortageSummaryFacadeService : IShortageSummaryFacadeService
    {
        private readonly IShortageSummaryReportService shortageSummaryReportService;

        public ShortageSummaryFacadeService(IShortageSummaryReportService shortageSummaryReportService)
        {
            this.shortageSummaryReportService = shortageSummaryReportService;
        }

        public IResult<ShortageSummary> ShortageSummaryByCit(string citCode, string ptlJobref)
        {
            if (string.IsNullOrEmpty(citCode))
            {
                return new BadRequestResult<ShortageSummary>("Must specify a cit code");
            }

            if (string.IsNullOrEmpty(ptlJobref))
            {
                return new BadRequestResult<ShortageSummary>("Must specify a PTL Jobref");
            }

            return new SuccessResult<ShortageSummary>(
                this.shortageSummaryReportService.ShortageSummaryByCit(citCode, ptlJobref));
        }
    }
}