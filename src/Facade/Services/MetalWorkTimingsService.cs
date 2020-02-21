namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class MetalWorkTimingsService : IMetalWorkTimingsService
    { 
        private readonly IMWTimingsReportService reportService;
        
        public MetalWorkTimingsService(IMWTimingsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetMetalWorkTimingsExport(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IResult<ResultsModel> GetMetalWorkTimingsReport(DateTime startDate, DateTime endDate)
        {
           return new SuccessResult<ResultsModel>(this.reportService.GetTimingsReport(startDate, endDate));
        }
    }
}
