namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Text.RegularExpressions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Extensions;
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

            foreach (var resultsColumn in results.Columns)
            {
                results.SetColumnType(resultsColumn.ColumnIndex, GridDisplayType.TextValue);
            }

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
            calculatedValues = calculatedValues.Where(v => !string.IsNullOrWhiteSpace(v.RowId));
            reportLayout.AddData(calculatedValues);

            var model = reportLayout.GetResultsModel();
            this.reportingHelper.SortRowsByColumnValue(model, model.ColumnIndex("Total"), true);
            model.ValueDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    this.GenerateValueDrillDown(groupBy, fromDate, toDate),
                    null,
                    model.ColumnIndex("Total")));
            model.ColumnDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    this.GenerateColumnDrillDown(groupBy, fromDate, toDate),
                    null,
                    model.ColumnIndex("Total")));
            return model;
        }

        public ResultsModel GetAssemblyFailsDetailsReport(
            DateTime fromDate,
            DateTime toDate,
            string boardPartNumber,
            string circuitPartNumber,
            string faultCode,
            string citCode,
            string board,
            int? person)
        {
            var weeks = this.linnWeekService.GetWeeks(fromDate, toDate).ToList();

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateDetailsReportTitle(boardPartNumber, fromDate, toDate, circuitPartNumber, faultCode, citCode, board));

            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("Week", GridDisplayType.TextValue),
                        new AxisDetailsModel("PartNumber", "Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("BoardPartNumber", "Board Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("Fails", GridDisplayType.TextValue),
                        new AxisDetailsModel("CircuitPartNumber", "Circuit Part Number", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("FaultCode", "Fault Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("ReportedFault", "Reported Fault", GridDisplayType.TextValue),
                        new AxisDetailsModel("Analysis", GridDisplayType.TextValue),
                        new AxisDetailsModel("Cit", GridDisplayType.TextValue)
                    });

            var filterQueries = this.GetAssemblyFailDataQueries(
                fromDate,
                toDate,
                boardPartNumber,
                circuitPartNumber,
                faultCode,
                citCode,
                board,
                person);
            var fails = this.GetFails(filterQueries);

            var values = new List<CalculationValueModel>();
            foreach (var assemblyFail in fails)
            {
                this.ExtractDetails(values, assemblyFail, weeks);
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();

            return model;
        }

        private IEnumerable<Expression<Func<AssemblyFail, bool>>> GetAssemblyFailDataQueries(
            DateTime fromDate,
            DateTime toDate,
            string boardPartNumber,
            string circuitPartNumber,
            string faultCode,
            string citCode,
            string board,
            int? person)
        {
            var expressions =
                new List<Expression<Func<AssemblyFail, bool>>>
                    {
                        f => f.DateTimeFound >= fromDate && f.DateTimeFound <= toDate
                    };

            if (!string.IsNullOrEmpty(boardPartNumber))
            {
                expressions.Add(f => f.BoardPartNumber == boardPartNumber);
            }

            if (!string.IsNullOrEmpty(circuitPartNumber))
            {
                expressions.Add(f => f.CircuitPart == circuitPartNumber);
            }

            if (!string.IsNullOrEmpty(faultCode))
            {
                expressions.Add(f => f.FaultCode != null && f.FaultCode.FaultCode == faultCode);
            }

            if (!string.IsNullOrEmpty(citCode))
            {
                expressions.Add(f => f.CitResponsible != null && f.CitResponsible.Code == citCode);
            }

            if (!string.IsNullOrEmpty(board))
            {
                expressions.Add(f => f.BoardPartNumber == board || (f.BoardPartNumber.IndexOf('/') >= 0 && f.BoardPartNumber.Substring(0, f.BoardPartNumber.IndexOf('/')) == board));
            }

            if (person.HasValue)
            {
                expressions.Add(f => f.PersonResponsible.Id == person);
            }

            return expressions;
        }

        private IEnumerable<AssemblyFail> GetFails(IEnumerable<Expression<Func<AssemblyFail, bool>>> expressions)
        {
            IQueryable<AssemblyFail> assemblyFails = null;
            foreach (var func in expressions)
            {
                assemblyFails = assemblyFails?.Where(func) ?? this.assemblyFailsRepository.FilterBy(func);
            }

            return assemblyFails?.OrderBy(a => a.Id).ToList() ?? new List<AssemblyFail>();
        }

        private void ExtractDetails(
            ICollection<CalculationValueModel> values,
            AssemblyFail assemblyFail,
            IEnumerable<LinnWeek> weeks)
        {
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "Week",
                        TextDisplay = this.linnWeekService.GetWeek(assemblyFail.DateTimeFound, weeks).WWSYY
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "PartNumber",
                        TextDisplay = assemblyFail.WorksOrder?.PartNumber
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "BoardPartNumber",
                        TextDisplay = assemblyFail.BoardPartNumber
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "Fails",
                        TextDisplay = assemblyFail.NumberOfFails.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "FaultCode",
                        TextDisplay = assemblyFail.FaultCode?.FaultCode
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "CircuitPartNumber",
                        TextDisplay = assemblyFail.CircuitPart
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "ReportedFault",
                        TextDisplay = assemblyFail.ReportedFault
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "Analysis",
                        TextDisplay = assemblyFail.Analysis
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = assemblyFail.Id.ToString(),
                        ColumnId = "Cit",
                        TextDisplay = assemblyFail.CitResponsible?.Name
                    });
        }

        private string GenerateDetailsReportTitle(
            string boardPartNumber,
            DateTime fromDate,
            DateTime toDate,
            string circuitPartNumber,
            string faultCode,
            string citCode,
            string board)
        {
            var title = $"Assembly fails between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}. ";
            if (!string.IsNullOrEmpty(boardPartNumber))
            {
                title += $"Board part number is {boardPartNumber} ";
            }


            if (!string.IsNullOrEmpty(circuitPartNumber))
            {
                title += $"Circuit part number is {circuitPartNumber} ";
            }

            if (!string.IsNullOrEmpty(faultCode))
            {
                title += $"Fault code is {faultCode} ";
            }

            if (!string.IsNullOrEmpty(citCode))
            {
                title += $"Cit code is {citCode} ";
            }

            if (!string.IsNullOrEmpty(board))
            {
                title += $"Board is {board} ";
            }

            return title;
        }

        private string GenerateValueDrillDown(AssemblyFailGroupBy groupBy, DateTime fromDate, DateTime toDate)
        {
            return $"/production/reports/assembly-fails-details?{char.ToLowerInvariant(groupBy.ToString()[0]) + groupBy.ToString().Substring(1)}={{rowId}}&parentGroupBy={groupBy.ParseOption()}&fromDate={WebUtility.UrlEncode(fromDate.ToString("o"))}&toDate={WebUtility.UrlEncode(toDate.ToString("o"))}";
        }

        private string GenerateColumnDrillDown(AssemblyFailGroupBy groupBy, DateTime fromDate, DateTime toDate)
        {
            return $"/production/reports/assembly-fails-details?parentGroupBy={groupBy.ParseOption()}&fromDate={WebUtility.UrlEncode(fromDate.ToString("o"))}&toDate={WebUtility.UrlEncode(toDate.ToString("o"))}";
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
                                     RowId = f.BoardPartNumber ?? string.Empty,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.FaultCode:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.FaultCode?.FaultCode ?? string.Empty,
                                     RowTitle = f.FaultCode?.Description,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.Board:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = string.IsNullOrEmpty(f.BoardPartNumber) ? string.Empty : f.BoardPartNumber.IndexOf('/') >= 0 ? f.BoardPartNumber.Substring(0, f.BoardPartNumber.IndexOf('/')) : f.BoardPartNumber,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                case AssemblyFailGroupBy.CitCode:
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.CitResponsible?.Code ?? string.Empty,
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
                    return fails.Select(
                        f => new CalculationValueModel
                                 {
                                     RowId = f.PersonResponsible?.Id.ToString() ?? string.Empty,
                                     RowTitle = f.PersonResponsible?.FullName ?? string.Empty,
                                     ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber.ToString(),
                                     Quantity = f.NumberOfFails
                                 });
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null);
            }
        }
    }
}
