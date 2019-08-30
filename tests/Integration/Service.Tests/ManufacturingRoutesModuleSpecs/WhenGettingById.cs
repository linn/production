namespace Linn.Production.Service.Tests.ManufacturingRoutesModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingManufacturingRoute : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var route = new ManufacturingRoute("TESTCODE", "desc", "a note");
            route.Operations = new List<ManufacturingOperation>();
            this.ManufacturingRouteService.GetById("TESTCODE")
                .Returns(new SuccessResult<ManufacturingRoute>(route));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-routes/TESTCODE",
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
            this.ManufacturingRouteService.Received().GetById("TESTCODE");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingRouteResource>();
            resource.RouteCode.Should().Be("TESTCODE");
            resource.Description.Should().Be("desc");
            resource.Notes.Should().Be("a note");
        }
    }
}
