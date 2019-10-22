namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OrdersReportsFacadeService : IOrdersReportsFacadeService
    {
        private readonly IOrdersReports ordersReportsService;

        private readonly IOverdueOrdersService overdueOrdersService;

        public OrdersReportsFacadeService(
            IOrdersReports ordersReportsService,
            IOverdueOrdersService overdueOrdersService)
        {
            this.ordersReportsService = ordersReportsService;
            this.overdueOrdersService = overdueOrdersService;
        }

        public IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date)
        {
            return new SuccessResult<ManufacturingCommitDateResults>(this.ordersReportsService.ManufacturingCommitDate(date));
        }

        public IResult<ResultsModel> GetOverdueOrdersReport(
            int jobId,
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod)
        {
            return new SuccessResult<ResultsModel>(
                this.overdueOrdersService.OverdueOrdersReport(
                    jobId,
                    fromDate,
                    toDate,
                    accountingCompany,
                    stockPool,
                    reportBy,
                    daysMethod));
        }
    }
}