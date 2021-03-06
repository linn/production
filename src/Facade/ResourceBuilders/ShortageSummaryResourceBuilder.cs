﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    public class ShortageSummaryResourceBuilder : IResourceBuilder<ShortageSummary>
    {
        private ShortageResultResourceBuilder shortageResultBuilder = new ShortageResultResourceBuilder();

        public ShortageSummaryReportResource BuildResource(ShortageSummary summary)
        {
            return new ShortageSummaryReportResource
            {
                ReportResults = new List<ShortageSummaryResource>()
                {
                    new ShortageSummaryResource
                    {
                        CitName = summary.CitName,
                        OnesTwos = summary.OnesTwos,
                        NumShortages = summary.NumShortages(),
                        BAT = summary.BAT(),
                        Metalwork = summary.Metalwork(),
                        Procurement = summary.Procurement(),
                        Shortages = summary.Shortages.Select(s => shortageResultBuilder.BuildResource(s)),
                        PercShortages = summary.ShortagePerc(),
                        PercBAT = summary.BATPerc(),
                        PercMetalwork = summary.MetalworkPerc(),
                        PercProcurement = summary.ProcurementPerc()
                    }
                }
            };
        }

        public object Build(ShortageSummary model) => this.BuildResource(model);

        public string GetLocation(ShortageSummary model)
        {
            throw new NotImplementedException();
        }
    }
}