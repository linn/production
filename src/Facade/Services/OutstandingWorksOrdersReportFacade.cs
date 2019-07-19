namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class OutstandingWorksOrdersReportFacade : IOutstandingWorksOrdersReportFacade
    {
        private readonly IOutstandingWorksOrdersReportService outstandingWorksOrdersReportService;

        public OutstandingWorksOrdersReportFacade(IOutstandingWorksOrdersReportService outstandingWorksOrdersReportService)
        {
            this.outstandingWorksOrdersReportService = outstandingWorksOrdersReportService;
        }

        public IResult<ResultsModel> GetOutstandingWorksOrdersReport()
        {
            var results = this.outstandingWorksOrdersReportService.GetOutstandingWorksOrders();
            return new SuccessResult<ResultsModel>(results);
        }
    }
}