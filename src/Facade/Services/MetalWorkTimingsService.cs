namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;

    public class MetalWorkTimingsService : IMetalWorkTimingsService
    { 
        private readonly IMWTimingsReportService reportService;
        
        public MetalWorkTimingsService(IMWTimingsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetMetalWorkTimingsExport(DateTime startDate, DateTime endDate)
        {
            var result = this.reportService.GetTimingsReport(startDate, endDate).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(result);
        }

        public IResult<ResultsModel> GetMetalWorkTimingsReport(DateTime startDate, DateTime endDate)
        {
           return new SuccessResult<ResultsModel>(this.reportService.GetTimingsReport(startDate, endDate));
        }
    }
}
