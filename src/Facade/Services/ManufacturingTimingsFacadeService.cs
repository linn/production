namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;

    public class ManufacturingTimingsFacadeService : IManufacturingTimingsFacadeService
    { 
        private readonly IManufacturingTimingsReportService reportService;
        
        public ManufacturingTimingsFacadeService(IManufacturingTimingsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetManufacturingTimingsExport(DateTime startDate, DateTime endDate, string citCode)
        {
            var result = this.reportService.GetTimingsReport(startDate, endDate, citCode).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(result);
        }

        public IResult<ResultsModel> GetManufacturingTimingsReport(DateTime startDate, DateTime endDate, string citCode)
        {
           return new SuccessResult<ResultsModel>(this.reportService.GetTimingsReport(startDate, endDate, citCode));
        }
    }
}
