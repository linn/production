namespace Linn.Production.Service.Tests.BuildPlansModule
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildPlans : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var buildPlans = new List<BuildPlan>
                                 {
                                     new BuildPlan { BuildPlanName = "B1" }, new BuildPlan { BuildPlanName = "B2" }
                                 };

            this.BuildPlanService.GetAll().Returns(new SuccessResult<IEnumerable<BuildPlan>>(buildPlans));

            this.Response = this.Browser.Get(
                "/production/maintenance/build-plans",
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
            this.BuildPlanService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<BuildPlanResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(b => b.BuildPlanName == "B1");
            resource.Should().Contain(b => b.BuildPlanName == "B2");
        }
    }
}
