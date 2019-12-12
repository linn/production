namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanRulesResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<BuildPlanRule>>>
    {
        private readonly BuildPlanRuleResourceBuilder buildPlanRuleResourceBuilder;

        public BuildPlanRulesResourceBuilder(IAuthorisationService authorisationService)
        {
            this.buildPlanRuleResourceBuilder = new BuildPlanRuleResourceBuilder(authorisationService);
        }

        public IEnumerable<BuildPlanRuleResource> Build(ResponseModel<IEnumerable<BuildPlanRule>> model)
        {
            var buildPlanRules = model.ResponseData;

            return buildPlanRules
                .OrderByDescending(b => b.RuleCode)
                .Select(b => this.buildPlanRuleResourceBuilder.Build(new ResponseModel<BuildPlanRule>(b, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<BuildPlanRule>>>.Build(ResponseModel<IEnumerable<BuildPlanRule>> buildPlanRules) =>
            this.Build(buildPlanRules);

        public string GetLocation(ResponseModel<IEnumerable<BuildPlanRule>> model)
        {
            throw new System.NotImplementedException();
        }
    }
}