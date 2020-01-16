namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class DeliveryPerfResultFacadeService : IDeliveryPerfResultFacadeService
    {
        private readonly IDeliveryPerformanceReportService deliveryPerformanceReportService;

        public DeliveryPerfResultFacadeService(IDeliveryPerformanceReportService deliveryPerformanceReportService)
        {
            this.deliveryPerformanceReportService = deliveryPerformanceReportService;
        }

        public IResult<ResultsModel> GenerateDelPerfSummaryForCit(string citCode)
        {
            if (string.IsNullOrEmpty(citCode))
            {
                return new BadRequestResult<ResultsModel>("Must specify a cit code");
            }

            return new SuccessResult<ResultsModel>(
                this.deliveryPerformanceReportService.GetDeliveryPerformanceByPriority(citCode));
        }
    }
}