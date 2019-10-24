namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OrdersReportsFacadeService : IOrdersReportsFacadeService
    {
        private readonly IManufacturingCommitDateReport manufacturingCommitDateReportService;

        private readonly IOverdueOrdersService overdueOrdersService;

        public OrdersReportsFacadeService(
            IManufacturingCommitDateReport manufacturingCommitDateReportService,
            IOverdueOrdersService overdueOrdersService)
        {
            this.manufacturingCommitDateReportService = manufacturingCommitDateReportService;
            this.overdueOrdersService = overdueOrdersService;
        }

        public IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date)
        {
            return new SuccessResult<ManufacturingCommitDateResults>(this.manufacturingCommitDateReportService.ManufacturingCommitDate(date));
        }

        public IResult<ResultsModel> GetOverdueOrdersReport(
            string reportBy,
            string daysMethod)
        {
            return new SuccessResult<ResultsModel>(
                this.overdueOrdersService.OverdueOrdersReport(
                    reportBy,
                    daysMethod));
        }
    }
}