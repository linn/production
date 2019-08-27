namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class AssemblyFailsWaitingListReportFacadeService : IAssemblyFailsWaitingListReportFacadeService
    {
        private readonly IAssemblyFailsWaitingListReportService reportService;

        public AssemblyFailsWaitingListReportFacadeService(IAssemblyFailsWaitingListReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<ResultsModel> GetAssemblyFailsWaitingListReport()
        {
            return new SuccessResult<ResultsModel>(this.reportService.GetAssemblyFailsWaitingListReport());
        }
    }
}