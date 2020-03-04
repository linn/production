namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps.Reports;

    public class PartsReportFacadeService : IPartsReportFacadeService
    {
        private readonly IPartsReportService partsReportService;

        public PartsReportFacadeService(IPartsReportService partsReportService)
        {
            this.partsReportService = partsReportService;
        }

        public IResult<ResultsModel> GetPartFailDetailsReport(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department)
        {
            return new SuccessResult<ResultsModel>(
                this.partsReportService.PartFailDetailsReport(
                    supplierId,
                    fromWeek,
                    toWeek,
                    errorType,
                    faultCode,
                    partNumber,
                    department));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetPartFailDetailsReportCsv(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department)
        {
            var results = this.partsReportService.PartFailDetailsReport(
                supplierId,
                fromWeek,
                toWeek,
                errorType,
                faultCode,
                partNumber,
                department).ConvertToCsvList();

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(results);
        }
    }
}
