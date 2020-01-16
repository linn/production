namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Services;

    public class DeliveryPeformanceReportService : IDeliveryPerformanceReportService
    {
        private readonly IQueryRepository<PtlStat> ptlStatRepository;

        private readonly ILinnWeekService linnWeekService;

        public DeliveryPeformanceReportService(IQueryRepository<PtlStat> ptlStatRepository, ILinnWeekService linnWeekService)
        {
            this.ptlStatRepository = ptlStatRepository;
            this.linnWeekService = linnWeekService;
        }

        public ResultsModel GetDeliveryPerformanceByPriority(string citCode)
        {
            var fromDate = this.linnWeekService.LinnWeekStartDate(DateTime.Now.AddDays(-28));
            var toDate = this.linnWeekService.LinnWeekEndDate(DateTime.Now.AddDays(-7));

            var model = new ResultsModel();
            model.ReportTitle = new NameModel("Production Delivery Performance " + fromDate.ToString("d") + " - " + toDate.ToString("d"));

            model.AddColumn("priority", "Priority");
            model.AddColumn("triggers", "Triggers");
            model.AddColumn("avgTurnaround", "Avg Turnaround");
            model.AddColumn("95Percentile", "95% Percentile");
            model.AddColumn("1day", "1 Day");
            model.AddColumn("2day", "2 Day");
            model.AddColumn("3day", "3 Day");
            model.AddColumn("4day", "4 Day");
            model.AddColumn("5day", "5 Day");
            model.AddColumn("percby5days", "% by 5 days");
            model.AddColumn("gt5day", "> 5 Day");

            return model;
        }
    }
}