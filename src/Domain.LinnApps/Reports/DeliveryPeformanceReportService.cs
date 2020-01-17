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
            var fromDate = this.linnWeekService.LinnWeekStartDate(DateTime.Now.AddDays(-28));
            var toDate = this.linnWeekService.LinnWeekEndDate(DateTime.Now.AddDays(-7)).AddHours(23).AddMinutes(59);

            var stats = this.ptlStatRepository.FilterBy(s =>
                s.CitCode == citCode && s.DateCompleted >= fromDate && s.DateCompleted <= toDate).ToList();

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

            /*
            foreach (var stat in stats)
            {
                if (summaries.ContainsKey(stat.PtlPriority))
                {
                    summaries[stat.PtlPriority].AddStatToSummary(stat);
                }
                else
                {
                    summaries.Add(stat.PtlPriority, new PtlStatPrioritySummary(stat));
                }
            }

            foreach (var summary in summaries.Values.OrderBy(s => s.Priority))
            {
                var row = model.AddRow(summary.Priority.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("priority"), summary.Priority.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("triggers"), summary.Triggers.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("avgTurnaround"), summary.AvgTurnaround().ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("95Percentile"), summary.Percentile95().ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("1day"), summary.OneDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("2day"), summary.TwoDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("3day"), summary.ThreeDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("4day"), summary.FourDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("5day"), summary.FiveDay.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("percBy5days"), summary.PercBy5Day().ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("gt5day"), summary.Gt5Day.ToString());
            }
            */

            return model;
        }
    }
}