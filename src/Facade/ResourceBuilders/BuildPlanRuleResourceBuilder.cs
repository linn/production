namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanRuleResourceBuilder : IResourceBuilder<BuildPlanRule>
    {
        public BuildPlanRuleResource Build(BuildPlanRule buildPlanRule)
        {
            return new BuildPlanRuleResource
                       {
                           Description = buildPlanRule.Description,
                           RuleCode = buildPlanRule.RuleCode,
                           Links = this.BuildLinks(buildPlanRule).ToArray()
                       };
        }

        public string GetLocation(BuildPlanRule buildPlanRule)
        {
            return $"/production/maintenance/build-plan-rules/{buildPlanRule.RuleCode}";
        }

        object IResourceBuilder<BuildPlanRule>.Build(BuildPlanRule buildPlanRule) => this.Build(buildPlanRule);

        private IEnumerable<LinkResource> BuildLinks(BuildPlanRule buildPlanRule)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(buildPlanRule) };
        }
    }
}
