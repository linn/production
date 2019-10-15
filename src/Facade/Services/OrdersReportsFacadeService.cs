namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OrdersReportsFacadeService : IOrdersReportsFacadeService
    {
        private readonly IOrdersReports ordersReportsService;

        public OrdersReportsFacadeService(IOrdersReports ordersReportsService)
        {
            this.ordersReportsService = ordersReportsService;
        }

        public IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date)
        {
            return new SuccessResult<ManufacturingCommitDateResults>(this.ordersReportsService.ManufacturingCommitDate(date));
        }

        public IResult<ResultsModel> GetOverdueOrdersReport(
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod)
        {
            throw new NotImplementedException();
        }
    }
}