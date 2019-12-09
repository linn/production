namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ptl1 = new ProductionTriggerLevel { PartNumber = "P1", Description = "d1" };
            this.ProductionTriggerLevelService.GetById("P1", Arg.Any<List<string>>())
                .Returns(new SuccessResult<ResponseModel<ProductionTriggerLevel>>(new ResponseModel<ProductionTriggerLevel>(ptl1, new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/production-trigger-levels/P1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ProductionTriggerLevelService.Received().GetById("P1", Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProductionTriggerLevel>();
            resource.PartNumber.Should().Be("P1");
        }
    }
}