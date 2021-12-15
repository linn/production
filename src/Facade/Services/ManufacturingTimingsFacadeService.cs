namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Reports;

    public class ManufacturingTimingsFacadeService : IManufacturingTimingsFacadeService
    { 
        private readonly IManufacturingTimingsReportService reportService;

        private readonly IBomService bomService;
        
        public ManufacturingTimingsFacadeService(IManufacturingTimingsReportService reportService,
                                                 IBomService bomService)
        {
            this.reportService = reportService;
            this.bomService = bomService;
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetManufacturingTimingsExport(DateTime startDate, DateTime endDate, string citCode)
        {
            var result = this.reportService.GetTimingsReport(startDate, endDate, citCode).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(result);
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetTimingsForAssembliesOnABom(string bomName)
        {
            var x = this.bomService.GetAllAssembliesOnBom(bomName).ToList();
            throw new NotImplementedException();
        }

        public IResult<ResultsModel> GetManufacturingTimingsReport(DateTime startDate, DateTime endDate, string citCode)
        {
           return new SuccessResult<ResultsModel>(this.reportService.GetTimingsReport(startDate, endDate, citCode));
        }
    }
}
