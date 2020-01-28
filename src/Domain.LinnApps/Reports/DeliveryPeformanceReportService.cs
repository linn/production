namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
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
            var dates = this.GetDefaultDateRange();

            var stats = this.ptlStatRepository.FilterBy(s =>
                s.CitCode == citCode && s.DateCompleted >= dates.fromDate && s.DateCompleted <= dates.toDate).ToList();

            var model = new ResultsModel();
            model.ReportTitle = new NameModel($"Production Delivery Performance {dates.fromDate.ToString("dd-MMM-yy")} - {dates.toDate.ToString("dd-MMM-yy")}");

            model.AddColumn("priority", "Priority");
            model.AddColumn("triggers", "Triggers");
            model.AddColumn("avgTurnaround", "Avg Turnaround");
            model.AddColumn("95Percentile", "95% Percentile");
            model.AddColumn("1day", "1 Day");
            model.AddColumn("2day", "2 Day");
            model.AddColumn("3day", "3 Day");
            model.AddColumn("4day", "4 Day");
            model.AddColumn("5day", "5 Day");
            model.AddColumn("percBy5days", "% by 5 days");
            model.AddColumn("gt5day", "> 5 Day");

            var priorities = stats.Select(s => s.PtlPriority).Distinct().OrderBy(s => s);

            foreach (var priority in priorities)
            {
                var summary = new PtlStatPrioritySummary(priority);
                var priorityStats = stats.Where(s => s.PtlPriority == priority).OrderBy(s => s.WorkingDays);
                var perc95count = priorityStats.Count() * 0.95;

                foreach (var statistic in priorityStats)
                {
                    summary.AddStatToSummary(statistic);
                    // TODO discover better way of doing 95% percentile as not too many internet answers
                    if (summary.Triggers <= perc95count)
                    {
                        summary.Percentile95 = statistic.WorkingDays;
                    }
                }

                var row = model.AddRow(summary.Priority.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("priority"), summary.Priority.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("triggers"), summary.Triggers.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("avgTurnaround"), summary.AvgTurnaround().ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("95Percentile"), summary.Percentile95.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("1day"), summary.OneDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("2day"), summary.TwoDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("3day"), summary.ThreeDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("4day"), summary.FourDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("5day"), summary.FiveDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("percBy5days"), summary.PercBy5Day().ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("gt5day"), summary.Gt5Day.ToString());
            }

            model.ValueDrillDownTemplates.Add(
                new DrillDownModel(
                    "Triggers",
                    "/production/reports/delperf/details?citCode="+citCode+"&priority={rowId}",
                    null,
                    model.ColumnIndex("triggers")));

            return model;
        }

        public ResultsModel GetDeliveryPerformanceDetail(string citCode, int priority)
        {
            var dates = this.GetDefaultDateRange();
            var stats = this.ptlStatRepository.FilterBy(s =>
                s.CitCode == citCode && s.PtlPriority == priority && s.DateCompleted >= dates.fromDate && s.DateCompleted <= dates.toDate).ToList();

            var model = new ResultsModel();
            model.ReportTitle = new NameModel($"Production Delivery Performance {dates.fromDate.ToString("dd-MMM-yy")} - {dates.toDate.ToString("dd-MMM-yy")} Priority {priority} Cit {citCode}");



            model.AddColumn("workingDays", "Working Days");
            model.AddColumn("partNumber", "Part Number");
            model.AddColumn("triggerDate", "Trigger Date");
            model.AddColumn("dateCompleted", "dateCompleted");
            model.AddColumn("triggerId", "Trigger Id");

            foreach (var stat in stats.OrderByDescending(s => s.WorkingDays))
            {
                var row = model.AddRow(stat.TriggerId.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("workingDays"), stat.WorkingDays.ToString("##0.0"));
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("partNumber"), stat.PartNumber);
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("triggerDate"), stat.TriggerDate?.ToString("dd-MMM-yy"));
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("dateCompleted"), stat.DateCompleted?.ToString("dd-MMM-yy"));
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("triggerId"), stat.TriggerId.ToString());
            }

            var priorities = stats.Select(s => s.PtlPriority).Distinct().OrderBy(s => s);
            return model;
        }

        private (DateTime fromDate, DateTime toDate) GetDefaultDateRange()
        {
            return (fromDate: this.linnWeekService.LinnWeekStartDate(DateTime.Now.AddDays(-28)),
                    toDate: this.linnWeekService.LinnWeekEndDate(DateTime.Now.AddDays(-7)).AddHours(23).AddMinutes(59));
        }
    }
}