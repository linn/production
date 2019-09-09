namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Layouts;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Domain.LinnApps.Services;

    public class AssemblyFailsReportService : IAssemblyFailsReportService
    {
        private readonly IRepository<AssemblyFail, int> assemblyFailsRepository;

        private readonly ILinnWeekService linnWeekService;

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekPack weekPack;

        public AssemblyFailsReportService(
            ILinnWeekPack weekPack,
            IRepository<AssemblyFail, int> assemblyFailRepository,
            ILinnWeekService linnWeekService,
            IReportingHelper reportingHelper)
        {
            this.assemblyFailsRepository = assemblyFailRepository;
            this.linnWeekService = linnWeekService;
            this.reportingHelper = reportingHelper;
            this.weekPack = weekPack;
        }

        public ResultsModel GetAssemblyFailsWaitingListReport()
        {
            var assemblyFails = this.assemblyFailsRepository
                .FindAll().Where(f => f.CompletedBy == null && f.DateInvalid == null)
                .OrderBy(f => f.Id);

            var results =
                new ResultsModel(
                    new[]
                        {
                            "Week",
                            "When Found",
                            "Part Number",
                            "Serial",
                            "Reported Fault",
                            "In Slot"
                        })
                    {
                        RowHeader = "Id",
                        ReportTitle = new NameModel("Assembly Fail Waiting List")
                    };

            foreach (var fail in assemblyFails.ToList())
            {
                this.weekPack.Wwsyy(fail.DateTimeFound);
                var row = results.AddRow(fail.Id.ToString());
                this.weekPack.Wwsyy(fail.DateTimeFound);
                results.SetGridTextValue(row.RowIndex, 0, this.weekPack.Wwsyy(fail.DateTimeFound));
                results.SetGridTextValue(row.RowIndex, 1, fail.DateTimeFound.ToString("d", new CultureInfo("en-GB")));
                results.SetGridTextValue(row.RowIndex, 2, fail.WorksOrder.PartNumber);
                results.SetGridTextValue(row.RowIndex, 3, fail.SerialNumber.ToString());
                results.SetGridTextValue(row.RowIndex, 4, fail.ReportedFault);
                results.SetGridTextValue(row.RowIndex, 5, fail.InSlot);
            }

            results.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/production/quality/assembly-fails/{rowId}"));
            return results;
        }

        public ResultsModel GetAssemblyFailsMeasuresReport(DateTime fromDate, DateTime toDate, AssemblyFailGroupBy groupBy)
        {
            var reportLayout = new ValuesByWeekLayout(this.reportingHelper, this.GenerateReportTitle(groupBy));
            var weeks = this.linnWeekService.GetWeeks(fromDate, toDate).ToList();
            reportLayout.AddWeeks(
                weeks.Select(
                    w => new AxisDetailsModel(w.LinnWeekNumber.ToString(), w.WeekEndingDDMON)
                             {
                                 SortOrder = w.LinnWeekNumber
                             }));
            var fails = this.assemblyFailsRepository.FilterBy(a => a.DateTimeFound >= fromDate && a.DateTimeFound <= toDate);

            var calculatedValues = this.CalculatedValues(fails, groupBy, weeks);
            reportLayout.AddData(calculatedValues);

            var model = reportLayout.GetResultsModel();
            this.reportingHelper.SortRowsByColumnValue(model, model.ColumnIndex("Total"), true);

            return model;
        }

        private string GenerateReportTitle(AssemblyFailGroupBy groupBy)
        {
            return $"Assembly Fails Measures Grouped By {Regex.Replace(groupBy.ToString(), "(\\B[A-Z])", " $1")}";
        }

        private IEnumerable<CalculationValueModel> CalculatedValues(
            IEnumerable<AssemblyFail> fails,
            AssemblyFailGroupBy groupBy,
            IReadOnlyCollection<LinnWeek> weeks)
        {
            switch (groupBy)
            {
                case AssemblyFailGroupBy.BoardPartNumber:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.BoardPartNumber,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.Fault:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.FaultCode?.FaultCode ?? string.Empty,
                                     RowTitle = f.FaultCode?.Description,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.Board:
                    break;
                case AssemblyFailGroupBy.Cit:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.CitResponsible?.Code,
                                     RowTitle = f.CitResponsible?.Name,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.CircuitPartNumber:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.CircuitPart ?? string.Empty,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.Person:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null);
            }

            return null;
        }
    }
}
