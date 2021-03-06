﻿namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System;
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

    public class WhenDeletingABuildPlanDetail : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            BuildPlanDetailResource requestResource =
                new BuildPlanDetailResource { BuildPlanName = "name", PartNumber = "part", FromDate = "2007-02-20" };

            var buildPlanDetail =
                new BuildPlanDetail { BuildPlanName = "name", PartNumber = "part", FromLinnWeekNumber = 1 };

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.BuildPlanDetailDelete, Arg.Any<List<string>>())
                .Returns(true);

            this.LinnWeekPack.LinnWeekNumber(Arg.Any<DateTime>()).Returns(1);

            this.BuildPlanDetailsService
                .RemoveBuildPlanDetail(Arg.Any<BuildPlanDetailResource>(), Arg.Any<IEnumerable<string>>()).Returns(
                    new SuccessResult<ResponseModel<BuildPlanDetail>>(
                        new ResponseModel<BuildPlanDetail>(buildPlanDetail, new List<string>())));

            this.Response = this.Browser.Delete(
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
            this.BuildPlanDetailsService.Received().RemoveBuildPlanDetail(
                Arg.Any<BuildPlanDetailResource>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanDetailResource>();
            resource.BuildPlanName.Should().Be("name");
            resource.PartNumber.Should().Be("part");
        }
    }
}