﻿namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Layouts;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class BuildPlansReportService : IBuildPlansReportService
    {
        private readonly IQueryRepository<BuildPlanDetailsReportLine> buildPlanDetailsLineRepository;

        private readonly ILinnWeekService linnWeekService;

        private readonly IReportingHelper reportingHelper;

        public BuildPlansReportService(
            IQueryRepository<BuildPlanDetailsReportLine> buildPlanDetailsLineRepository,
            ILinnWeekService linnWeekService,
            IReportingHelper reportingHelper)
        {
            this.buildPlanDetailsLineRepository = buildPlanDetailsLineRepository;
            this.linnWeekService = linnWeekService;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel BuildPlansReport(string buildPlanName, int weeks, string citName)
        {
            var reportLayout = new ValuesByWeekLayout(this.reportingHelper, "Build Plan Report");

            var from = this.linnWeekService.GetWeek(DateTime.Now).LinnWeekNumber;

            var to = this.linnWeekService.GetWeek(DateTime.Now.AddDays(weeks * 7)).LinnWeekNumber;

            var allWeeks = this.linnWeekService.GetWeeks(DateTime.Now, DateTime.Now.AddDays(weeks * 7)).ToList();

            var buildPlans = citName == "ALL"
                                 ? this.buildPlanDetailsLineRepository.FilterBy(
                                     b => b.BuildPlanName == buildPlanName && b.LinnWeekNumber >= from
                                                                           && b.LinnWeekNumber <= to)
                                 : this.buildPlanDetailsLineRepository.FilterBy(
                                     b => b.BuildPlanName == buildPlanName && b.LinnWeekNumber >= from
                                                                           && b.LinnWeekNumber <= to
                                                                           && b.CitName == citName);

            reportLayout.AddWeeks(
                allWeeks.Select(
                    w => new AxisDetailsModel(w.LinnWeekNumber.ToString(), w.WeekEndingDDMON)
                             {
                                 SortOrder = w.LinnWeekNumber
                             }));

            var calculatedValues = buildPlans.Select(
                b => new CalculationValueModel
                         {
                             RowId = b.PartNumber,
                             RowTitle = b.PartNumber,
                             ColumnId = b.LinnWeekNumber.ToString(),
                             TextDisplay = b.FixedBuild.ToString()
                         });

            reportLayout.AddData(calculatedValues);

            var model = reportLayout.GetResultsModel();

            model.RowHeader = "Part Number";
            
            this.reportingHelper.SortRowsByRowTitle(model);

            return model;
        }
    }
}