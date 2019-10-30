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

    public class WhenAddingManufacturingRoute : ContextBase
    {
        private ManufacturingRouteResource requestRoute;

        [SetUp]
        public void SetUp()
        {
            this.requestRoute = new ManufacturingRouteResource() { RouteCode = "ADD TEST", Description = "Descrip", Notes = "some extra info" };
            var newRoute = new ManufacturingRoute("ADD TEST", "Descrip", "some extra info");

            this.ManufacturingRouteService.Add(Arg.Any<ManufacturingRouteResource>())
                .Returns(new CreatedResult<ManufacturingRoute>(newRoute));

            this.Response = this.Browser.Post(
                "/production/resources/manufacturing-routes",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestRoute);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ManufacturingRouteService.Received()
                .Add(Arg.Is<ManufacturingRouteResource>(r => r.RouteCode == this.requestRoute.RouteCode));
        }

        [Test]
        public void ShouldReturnRoute()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingRouteResource>();
            resource.RouteCode.Should().Be("ADD TEST");
            resource.Description.Should().Be("Descrip");
            resource.Notes.Should().Be("some extra info");
        }
    }
}
