namespace Linn.Production.Service.Tests.ManufacturingResourcesModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingManufacturingResources : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new ManufacturingResource { ResourceCode = "a", Description = "desc", Cost = 15, DateInvalid = null };
            var b = new ManufacturingResource { ResourceCode = "b", Description = "desc", Cost = 17, DateInvalid = null };
            var c = new ManufacturingResource { ResourceCode = "c", Description = "desc", Cost = 19, DateInvalid = DateTime.Today };
            var d = new ManufacturingResource { ResourceCode = "d", Description = "desc", Cost = 21, DateInvalid = DateTime.Today };
            this.ManufacturingResourceFacadeService.FilterBy(Arg.Any<ManufacturingResourceResource>())
                .Returns(new SuccessResult<IEnumerable<ManufacturingResource>>(new List<ManufacturingResource> { a, b }));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-resources",
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
            this.ManufacturingResourceFacadeService.Received().FilterBy(Arg.Any<ManufacturingResourceResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ManufacturingResourceResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.ResourceCode == "a");
            resources.Should().Contain(a => a.ResourceCode == "b");
        }
    }
}
