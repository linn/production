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

    public class WhenUpdatingManufacturingRoute : ContextBase
    {
        private ManufacturingRouteResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ManufacturingRouteResource() { RouteCode = "MYTEST", Description = "Desc1", Notes = "extra info" };
            var skill = new ManufacturingRoute("MYTEST", "Desc1", "extra info");
            this.ManufacturingRouteService.Update("MYTEST", Arg.Any<ManufacturingRouteResource>())
                .Returns(new SuccessResult<ManufacturingRoute>(skill));

            this.Response = this.Browser.Put(
                "/production/resources/manufacturing-routes/MYTEST",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.ManufacturingRouteService.Received()
                .Update("MYTEST", Arg.Is<ManufacturingRouteResource>(r => r.RouteCode == this.requestResource.RouteCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingRouteResource>();
            resource.RouteCode.Should().Be("MYTEST");
            resource.Description.Should().Be("Desc1");
            resource.Notes.Should().Be("extra info");
        }
    }
}
