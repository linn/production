namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanRulesResourceBuilder : IResourceBuilder<IEnumerable<BuildPlanRule>>
    {
        private readonly BuildPlanRuleResourceBuilder buildPlanRuleResourceBuilder = new BuildPlanRuleResourceBuilder();

        public IEnumerable<BuildPlanRuleResource> Build(IEnumerable<BuildPlanRule> buildPlanRules)
        {
            return buildPlanRules.OrderByDescending(b => b.RuleCode)
                .Select(b => this.buildPlanRuleResourceBuilder.Build(b));
        }

        object IResourceBuilder<IEnumerable<BuildPlanRule>>.Build(IEnumerable<BuildPlanRule> buildPlanRules) =>
            this.Build(buildPlanRules);

        public string GetLocation(IEnumerable<BuildPlanRule> model)
        {
            throw new System.NotImplementedException();
        }
    }
}