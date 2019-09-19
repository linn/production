﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class PtlSettingsResourceBuilder : IResourceBuilder<ResponseModel<PtlSettings>>
    {
        public PtlSettingsResource Build(ResponseModel<PtlSettings> model)
        {
            return new PtlSettingsResource
                       {
                           BuildToMonthEndFromDays = model.ResponseData.BuildToMonthEndFromDays,
                           DaysToLookAhead = model.ResponseData.DaysToLookAhead,
                           PriorityCutOffDays = model.ResponseData.PriorityCutOffDays,
                           SubAssemblyDaysToLookAhead = model.ResponseData.SubAssemblyDaysToLookAhead,
                           FinalAssemblyDaysToLookAhead = model.ResponseData.FinalAssemblyDaysToLookAhead,
                           PriorityStrategy = model.ResponseData.PriorityStrategy,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        object IResourceBuilder<ResponseModel<PtlSettings>>.Build(ResponseModel<PtlSettings> model) => this.Build(model);

        public string GetLocation(ResponseModel<PtlSettings> model)
        {
            return "/production/maintenance/production-trigger-levels-settings";
        }

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<PtlSettings> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };
        }
    }
}
