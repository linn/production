﻿namespace Linn.Production.Facade.Tests.BuildPlanDetailsServiceSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRemovingABuildPlanDetail : ContextBase
    {
        private IResult<ResponseModel<BuildPlanDetail>> result;

        private BuildPlanDetailResource resource;

        private BuildPlanDetail buildPlanDetail;

        [SetUp]
        public void SetUp()
        {
            this.resource = new BuildPlanDetailResource
                                {
                                    PartNumber = "PART",
                                    BuildPlanName = "NAME",
                                    FromDate = new DateTime(2020, 01, 30).ToString("o")
                                };

            this.buildPlanDetail =
                new BuildPlanDetail { PartNumber = "PART", BuildPlanName = "NAME", FromLinnWeekNumber = 22 };

            this.BuildPlanDetailRepository.FindById(Arg.Any<BuildPlanDetailKey>()).Returns(this.buildPlanDetail);

            this.result = this.Sut.RemoveBuildPlanDetail(this.resource, new List<string>());
        }

        [Test]
        public void ShouldGetBuildPlanDetail()
        {
            this.BuildPlanDetailRepository.Received().FindById(Arg.Any<BuildPlanDetailKey>());
        }

        [Test]
        public void ShouldRemoveBuildPlanDetail()
        {
            this.BuildPlanDetailRepository.Received().Remove(this.buildPlanDetail);
        }

        [Test]
        public void ShouldReturnBuildPlanDetail()
        {
            var dataResult = ((SuccessResult<ResponseModel<BuildPlanDetail>>)this.result).Data;
            dataResult.ResponseData.PartNumber.Should().Be("PART");
            dataResult.ResponseData.BuildPlanName.Should().Be("NAME");
        }
    }
}