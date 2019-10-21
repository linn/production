namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OrdersReportsFacadeService : IOrdersReportsFacadeService
    {
        private readonly IManufacturingCommitDateReport manufacturingCommitDateReportService;

        public OrdersReportsFacadeService(IManufacturingCommitDateReport manufacturingCommitDateReportService)
        {
            this.manufacturingCommitDateReportService = manufacturingCommitDateReportService;
        }

        public IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date)
        {
            return new SuccessResult<ManufacturingCommitDateResults>(this.manufacturingCommitDateReportService.ManufacturingCommitDate(date));
        }
    }
}