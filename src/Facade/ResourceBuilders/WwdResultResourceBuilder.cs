﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WwdResultResourceBuilder : IResourceBuilder<WwdResult>
    {
        private readonly WwdDetailResourceBuilder detailResourceBuilder = new WwdDetailResourceBuilder();

        public object Build(WwdResult result)
        {
            return new WwdResultReportResource
            {
                ReportResults = new WwdResultResource
                {
                    PartNumber = result.PartNumber,
                    WwdJobId = result.WwdJobId,
                    WwdRunDatetime = result.WwdRunTime.ToString("o"),
                    Qty = result.Qty,
                    WorkStationCode = result.WorkStationCode,
                    WwdDetails = result.WwdDetails.Select(d => this.detailResourceBuilder.BuildDetail(d))
                }
            };
        }

        public string GetLocation(WwdResult model)
        {
            throw new System.NotImplementedException();
        }
    }
}