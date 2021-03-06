﻿namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class WwdResultReportResponseProcessor : JsonResponseProcessor<WwdResult>
    {
        public WwdResultReportResponseProcessor(IResourceBuilder<WwdResult> resourceBuilder)
            : base(resourceBuilder, "wwd-result-report", 1)
        {
        }
    }
}