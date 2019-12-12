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

    public class WhenUpdatingABuildPlan : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            BuildPlanResource requestResource = new BuildPlanResource { BuildPlanName = "name", Description = "desc" };

            var buildPlan = new BuildPlan { BuildPlanName = "name", Description = "desc" };

            this.BuildPlanFacadeService.Update("name", Arg.Any<BuildPlanResource>())
                .Returns(new SuccessResult<BuildPlan>(buildPlan));

            this.Response = this.Browser.Put(
                "/production/maintenance/build-plans",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.BuildPlanFacadeService.Received().Update("name", Arg.Any<BuildPlanResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanResource>();
            resource.BuildPlanName.Should().Be("name");
            resource.Description.Should().Be("desc");
        }
    }

    public class WhenUpdatingABuildPlanDetail : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            BuildPlanDetailResource requestResource =
                new BuildPlanDetailResource { BuildPlanName = "name", PartNumber = "part", FromLinnWeekNumber = 1 };

            var buildPlanDetail =
                new BuildPlanDetail { BuildPlanName = "name", PartNumber = "part", FromLinnWeekNumber = 1 };

            this.BuildPlanDetailsFacadeService.Update(Arg.Any<BuildPlanDetailKey>(), Arg.Any<BuildPlanDetailResource>())
                .Returns(new SuccessResult<BuildPlanDetail>(buildPlanDetail));

            this.Response = this.Browser.Put(
                "/production/maintenance/build-plan-details",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.BuildPlanDetailsFacadeService.Received().Update(
                Arg.Any<BuildPlanDetailKey>(),
                Arg.Any<BuildPlanDetailResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanDetailResource>();
            resource.BuildPlanName.Should().Be("name");
            resource.PartNumber.Should().Be("part");
            resource.FromLinnWeekNumber.Should().Be(1);
        }
    }
}