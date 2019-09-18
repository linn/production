namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ptl1 = new ProductionTriggerLevel { PartNumber = "pcas1", Description = "d1" };
            var ptl2 = new ProductionTriggerLevel { PartNumber = "pcas2", Description = "d2" };
            this.ProductionTriggerLevelService.GetAll()
                .Returns(new SuccessResult<IEnumerable<ProductionTriggerLevel>>(new List<ProductionTriggerLevel> { ptl1, ptl2 }));

            this.Response = this.Browser.Get(
                "/production/maintenance/production-trigger-levels",
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
            this.ProductionTriggerLevelService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ProductionTriggerLevel>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.PartNumber == "pcas1");
            resources.Should().Contain(a => a.PartNumber == "pcas2");
        }
    }
}