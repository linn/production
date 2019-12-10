namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllBuildPlanRules : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new BuildPlanRule { Description = "d1", RuleCode = "r1" };
            var b = new BuildPlanRule { Description = "d2", RuleCode = "r2" };

            this.BuildPlanRulesFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<BuildPlanRule>>(new List<BuildPlanRule> { a, b }));

            this.Response = this.Browser.Get(
                "/production/maintenance/build-plan-rules",
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
            this.BuildPlanRulesFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<BuildPlanRuleResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(r => r.RuleCode == "r1");
            resources.Should().Contain(r => r.RuleCode == "r2");
        }
    }
}