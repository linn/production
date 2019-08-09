namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Resources;

    public class OutstandingWorksOrdersReportFacade : IOutstandingWorksOrdersReportFacade
    {
        private readonly IOutstandingWorksOrdersReportService outstandingWorksOrdersReportService;

        public OutstandingWorksOrdersReportFacade(IOutstandingWorksOrdersReportService outstandingWorksOrdersReportService)
        {
            this.outstandingWorksOrdersReportService = outstandingWorksOrdersReportService;
        }

        public IResult<ResultsModel> GetOutstandingWorksOrdersReport(string reportType, string searchParameter)
        {
            var results = this.outstandingWorksOrdersReportService.GetOutstandingWorksOrders(reportType, searchParameter);
            return new SuccessResult<ResultsModel>(results);
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetOutstandingWorksOrdersReportCsv(string reportType, string searchParameter)
        {
            var results = this.outstandingWorksOrdersReportService.GetOutstandingWorksOrders(reportType, searchParameter).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(results);
        }
    }
}