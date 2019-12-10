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

    public class WhenGettingAllBuildPlans : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new BuildPlan { BuildPlanName = "a" };
            var b = new BuildPlan { BuildPlanName = "b" };

            this.BuildPlanFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<BuildPlan>>(new List<BuildPlan> { a, b }));

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
            this.BuildPlanFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<BuildPlanResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(r => r.BuildPlanName == "a");
            resources.Should().Contain(r => r.BuildPlanName == "b");
        }
    }
}
