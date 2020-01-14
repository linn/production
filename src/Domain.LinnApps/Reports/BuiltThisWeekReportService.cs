namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Linq;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class BuiltThisWeekReportService : IBuiltThisWeekReportService
    {
        private readonly IQueryRepository<BuiltThisWeekStatistic> builtThisWeekStatisticRepository;

        public BuiltThisWeekReportService(IQueryRepository<BuiltThisWeekStatistic> builtThisWeekStatisticRepository)
        {
            this.builtThisWeekStatisticRepository = builtThisWeekStatisticRepository;
        }

        public ResultsModel GetBuiltThisWeekReport(string citCode)
        {
            var results = this.builtThisWeekStatisticRepository.FilterBy(c => c.CitCode == citCode).ToList();

            var model = new ResultsModel { ReportTitle = new NameModel("Built this Week Detail") };

            model.AddColumn("partNumber", "Part Number");
            model.AddColumn("description", "Description");
            model.AddColumn("builtThisWeek", "Built This Week");
            model.AddColumn("value", "Value");
            model.AddColumn("days", "Days");
            model.RowHeader = results.FirstOrDefault()?.CitName;

            foreach (var statistic in results)
            {
                var row = model.AddRow(statistic.PartNumber);
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("partNumber"), statistic.PartNumber);
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("description"), statistic.Description);
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("builtThisWeek"), statistic.BuiltThisWeek.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("value"), statistic.Value.ToString());
                model.SetGridTextValue(row.RowIndex, model.ColumnIndex("days"), statistic.Days.ToString());
            }

            return model;
        }
    }
}