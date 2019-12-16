﻿namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
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

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.BuildPlanUpdate, Arg.Any<List<string>>())
                .Returns(true);

            this.BuildPlanFacadeService.Update("name", Arg.Any<BuildPlanResource>(), Arg.Any<IEnumerable<string>>())
                .Returns(
                    new SuccessResult<ResponseModel<BuildPlan>>(
                        new ResponseModel<BuildPlan>(buildPlan, new List<string>())));

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
            this.BuildPlanFacadeService.Received().Update(
                "name",
                Arg.Any<BuildPlanResource>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanResource>();
            resource.BuildPlanName.Should().Be("name");
            resource.Description.Should().Be("desc");
        }
    }
}