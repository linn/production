namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanRuleResourceBuilder : IResourceBuilder<ResponseModel<BuildPlanRule>>
    {
        private readonly IAuthorisationService authorisationService;

        public BuildPlanRuleResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public BuildPlanRuleResource Build(ResponseModel<BuildPlanRule> model)
        {
            var buildPlanRule = model.ResponseData;

            return new BuildPlanRuleResource
                       {
                           Description = buildPlanRule.Description,
                           RuleCode = buildPlanRule.RuleCode,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<BuildPlanRule> model)
        {
            return $"/production/maintenance/build-plan-rules/{model.ResponseData.RuleCode}";
        }

        object IResourceBuilder<ResponseModel<BuildPlanRule>>.Build(ResponseModel<BuildPlanRule> buildPlanRule) =>
            this.Build(buildPlanRule);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<BuildPlanRule> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanAdd, model.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = this.GetLocation(model) };

                yield return new LinkResource { Rel = "edit", Href = this.GetLocation(model) };
            }
        }
    }
}
