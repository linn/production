namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Extensions;
    using Linn.Production.Resources.RequestResources;

    public class AssemblyFailsReportsFacadeService : IAssemblyFailsReportsFacadeService
    {
        private readonly IAssemblyFailsReportService reportService;

        public AssemblyFailsReportsFacadeService(IAssemblyFailsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<ResultsModel> GetAssemblyFailsWaitingListReport()
        {
            return new SuccessResult<ResultsModel>(this.reportService.GetAssemblyFailsWaitingListReport());
        }

        public IResult<ResultsModel> GetAssemblyFailsMeasuresReport(string fromDate, string toDate, string groupBy)
        {
            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.UtcNow.Date.ToString("O");
            }

            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Parse(toDate).AddMonths(-4).Date.ToString("O");
            }

            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(fromDate);
                to = DateTime.Parse(toDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to assembly fails measures report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetAssemblyFailsMeasuresReport(from, to, groupBy.ParseOption()));
        }

        public IResult<ResultsModel> GetAssemblyFailsDetailsReport(AssemblyFailsDetailsReportRequestResource resource)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(resource.FromDate);
                to = DateTime.Parse(resource.ToDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to assembly fails details report");
            }

            return new SuccessResult<ResultsModel>(
                this.reportService.GetAssemblyFailsDetailsReport(
                    from,
                    to,
                    resource.BoardPartNumber,
                    resource.CircuitPartNumber,
                    resource.FaultCode,
                    resource.CitCode,
                    resource.Board,
                    resource.Person));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetAssemblyFailsDetailsReportExport(AssemblyFailsDetailsReportRequestResource resource)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(resource.FromDate);
                to = DateTime.Parse(resource.ToDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<IEnumerable<IEnumerable<string>>>("Invalid dates supplied to assembly fails details report");
            }

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.reportService.GetAssemblyFailsDetailsReportExport(from, to).ConvertToCsvList());
        }
    }
}