namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class PartsReportFacadeService : IPartsReportFacadeService
    {
        private IPartsReportService partsReportService;

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
    }
}
