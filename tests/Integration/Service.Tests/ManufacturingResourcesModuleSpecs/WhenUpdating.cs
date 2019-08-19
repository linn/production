namespace Linn.Production.Service.Tests.ManufacturingResourcesModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingManufacturingResource : ContextBase
    {
        private ManufacturingResourceResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ManufacturingResourceResource() { ResourceCode = "MYTEST", Description = "Desc1", Cost = 150 };
            var skill = new ManufacturingResource("MYTEST", "Desc1", 150);
            this.ManufacturingResourceService.Update("MYTEST", Arg.Any<ManufacturingResourceResource>())
                .Returns(new SuccessResult<ManufacturingResource>(skill));

            this.Response = this.Browser.Put(
                "/production/resources/manufacturing-resources/MYTEST",
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
            this.ManufacturingResourceService.Received()
                .Update("MYTEST", Arg.Is<ManufacturingResourceResource>(r => r.ResourceCode == this.requestResource.ResourceCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingResourceResource>();
            resource.ResourceCode.Should().Be("MYTEST");
            resource.Description.Should().Be("Desc1");
            resource.Cost.Should().Be(150);
        }
    }
}
