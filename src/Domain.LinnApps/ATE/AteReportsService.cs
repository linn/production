namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Layouts;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Domain.LinnApps.Services;

    public class AteReportsService : IAteReportsService
    {
        private readonly IRepository<AteTest, int> ateTestRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekService linnWeekService;

        public AteReportsService(
            IRepository<AteTest, int> ateTestRepository,
            IReportingHelper reportingHelper,
            ILinnWeekService linnWeekService)
        {
            this.ateTestRepository = ateTestRepository;
            this.reportingHelper = reportingHelper;
            this.linnWeekService = linnWeekService;
        }

        public ResultsModel GetStatusReport(
            DateTime fromDate,
            DateTime toDate,
            string smtOrPcb,
            string placeFound,
            AteReportGroupBy groupBy)
        {
            var data = this.ateTestRepository.FilterBy(
                a => a.DateTested != null
                     && a.DateTested.Value.Date >= fromDate.Date
                     && a.DateTested.Value.Date <= toDate.Date);
            if (!string.IsNullOrEmpty(placeFound))
            {
                data = data.Where(d => d.PlaceFound == placeFound);
            }

            var details = data.SelectMany(a => a.Details).ToList();

            if (!string.IsNullOrEmpty(smtOrPcb))
            {
                details = details.Where(a => a.SmtOrPcb == smtOrPcb).ToList();
            }

            var reportLayout = new ValuesByWeekLayout(
                this.reportingHelper,
                this.GenerateReportTitle(groupBy),
                null,
                false);
            var weeks = this.linnWeekService.GetWeeks(fromDate, toDate).ToList();
            reportLayout.AddWeeks(
                weeks.Select(
                    w => new AxisDetailsModel(w.LinnWeekNumber.ToString(), w.WeekEndingDDMON)
                             {
                                 SortOrder = w.LinnWeekNumber
                             }));

            reportLayout.AddData(this.CalculateValues(details, data.ToList(), groupBy, weeks));

            var model = reportLayout.GetResultsModel();
            this.reportingHelper.SortRowsByRowTitle(model);

            return model;
        }

        public ResultsModel GetDetailsReport(DateTime fromDate, DateTime toDate, string selectBy, string value)
        {
            throw new NotImplementedException();
        }

        private string GenerateReportTitle(AteReportGroupBy groupBy)
        {
            return $"ATE Test Fails By {Regex.Replace(groupBy.ToString(), "(\\B[A-Z])", " $1")}";
        }

        private IEnumerable<CalculationValueModel> CalculateValues(
            IEnumerable<AteTestDetail> details,
            IList<AteTest> tests,
            AteReportGroupBy groupBy,
            IReadOnlyCollection<LinnWeek> weeks)
        {
            switch (groupBy)
            {
                case AteReportGroupBy.Component:
                    return details.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.PartNumber,
                                     ColumnId = this.linnWeekService.GetWeek(tests.FirstOrDefault(t => t.TestId == f.TestId).DateTested.Value, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails ?? 0
                                 });
                case AteReportGroupBy.FaultCode:
                    return details.Select(
                        f => new CalculationValueModel
                        {
                            RowId = f.AteTestFaultCode ?? string.Empty,
                            ColumnId = this.linnWeekService.GetWeek(tests.FirstOrDefault(t => t.TestId == f.TestId).DateTested.Value, weeks).LinnWeekNumber.ToString(),
                            Quantity = f.NumberOfFails ?? 0
                        });
                case AteReportGroupBy.Board:
                    return details.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = tests.FirstOrDefault(t => t.TestId == f.TestId).WorksOrder.PartNumber ?? string.Empty,
                                     ColumnId = this.linnWeekService.GetWeek(tests.FirstOrDefault(t => t.TestId == f.TestId).DateTested.Value, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails ?? 0
                                 });
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null);
            }
        }
    }
}