namespace Linn.Production.Service.Tests.ManufacturingRoutesModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class WhenGettingManufacturingRoutes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new ManufacturingRoute("code1", "desc1", "a note");
            var b = new ManufacturingRoute("code2", "desc2", "second note");
            a.Operations = new List<ManufacturingOperation>();
            b.Operations = new List<ManufacturingOperation>();

            this.ManufacturingRouteService.GetAll()
                .Returns(new SuccessResult<IEnumerable<ManufacturingRoute>>(new List<ManufacturingRoute> { a, b }));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-routes",
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
            this.ManufacturingRouteService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ManufacturingRouteResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.RouteCode == "code1");
            resources.Should().Contain(a => a.RouteCode == "code2");
        }
    }
}
