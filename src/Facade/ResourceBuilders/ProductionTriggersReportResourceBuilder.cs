﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class ProductionTriggersReportResourceBuilder : IResourceBuilder<ProductionTriggersReport>
    {
        private readonly ProductionTriggerSummaryResourceBuilder summaryResourceBuilder = new ProductionTriggerSummaryResourceBuilder();

        public object Build(ProductionTriggersReport report)
        {
            return new ProductionTriggerReportResultsResource
            {
                ReportResults = new ProductionTriggersReportResource()
                {
                    CitCode = report.Cit?.Code,
                    CitName = report.Cit?.Name,
                    PtlJobref = report.PtlMaster?.LastFullRunJobref,
                    PtlRunDateTime = report.PtlMaster?.LastFullRunDateTime.ToString("o"),
                    Triggers = report.Triggers.Select(t => this.summaryResourceBuilder.BuildSummary(t))
                }
            };
        }

        public string GetLocation(ProductionTriggersReport model)
        {
            return "/production/reports/triggers";
        }
    }
}