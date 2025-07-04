﻿namespace Linn.Production.Service.Tests.ManufacturingResourcesModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingManufacturingResources : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new ManufacturingResource("a", "desc", 15);
            var b = new ManufacturingResource("b", "desc", 17);

            this.ManufacturingResourceFacadeService.FilterBy(Arg.Any<IncludeInvalidRequestResource>())
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
            this.ManufacturingResourceFacadeService.Received().FilterBy(Arg.Any<IncludeInvalidRequestResource>());
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
