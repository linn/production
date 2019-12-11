namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildPlanRule : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var buildPlanRule = new BuildPlanRule { Description = "desc", RuleCode = "rule" };

            this.BuildPlanRulesFacadeService.GetById("rule").Returns(new SuccessResult<BuildPlanRule>(buildPlanRule));

            this.Response = this.Browser.Get(
                "/production/maintenance/build-plan-rules/rule",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.BuildPlanRulesFacadeService.Received().GetById("rule");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanRuleResource>();
            resource.RuleCode.Should().Be("rule");
            resource.Description.Should().Be("desc");
        }
    }
}