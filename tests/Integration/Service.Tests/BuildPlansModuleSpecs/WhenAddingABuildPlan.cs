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

    public class WhenAddingABuildPlan : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new BuildPlanResource { BuildPlanName = "name" };

            var buildPlan = new BuildPlan { BuildPlanName = "name" };

            this.BuildPlanFacadeService.Add(Arg.Any<BuildPlanResource>())
                .Returns(new CreatedResult<BuildPlan>(buildPlan));

            this.Response = this.Browser.Post(
                "/production/maintenance/build-plans",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.BuildPlanFacadeService.Received().Add(Arg.Any<BuildPlanResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanResource>();
            resource.BuildPlanName.Should().Be("name");
        }
    }
}