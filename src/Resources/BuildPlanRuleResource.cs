namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class BuildPlanRuleResource : HypermediaResource
    {
        public string RuleCode { get; set; }

        public string Description { get; set; }
    }
}
