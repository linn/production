namespace Linn.Production.Facade.Services
{
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OrdersReportsFacadeService : IOrdersReportsFacadeService
    {
        private readonly IManufacturingCommitDateReport manufacturingCommitDateReportService;

        private readonly IOverdueOrdersService overdueOrdersService;

        private readonly IProductionBackOrdersReportService productionBackOrdersReportService;

        public OrdersReportsFacadeService(
            IManufacturingCommitDateReport manufacturingCommitDateReportService,
            IOverdueOrdersService overdueOrdersService,
            IProductionBackOrdersReportService productionBackOrdersReportService)
        {
            this.manufacturingCommitDateReportService = manufacturingCommitDateReportService;
            this.overdueOrdersService = overdueOrdersService;
            this.productionBackOrdersReportService = productionBackOrdersReportService;
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

        public IResult<IEnumerable<ResultsModel>> ProductionBackOrdersReport(string citCode)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.productionBackOrdersReportService.ProductionBackOrders(citCode));
        }
    }
}