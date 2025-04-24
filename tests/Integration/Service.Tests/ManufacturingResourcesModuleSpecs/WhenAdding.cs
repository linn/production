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

    public class WhenAddingManufacturingResource : ContextBase
    {
        private ManufacturingResourceResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ManufacturingResourceResource { ResourceCode = "ADD TEST", Description = "Descrip", Cost = 151 };
            var newResource = new ManufacturingResource("ADD TEST", "Descrip", 151, null);

            this.ManufacturingResourceFacadeService.Add(Arg.Any<ManufacturingResourceResource>())
                .Returns(new CreatedResult<ManufacturingResource>(newResource));

            this.Response = this.Browser.Post(
                "/production/resources/manufacturing-resources",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ManufacturingResourceFacadeService.Received()
                .Add(Arg.Is<ManufacturingResourceResource>(r => r.ResourceCode == this.requestResource.ResourceCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingResourceResource>();
            resource.ResourceCode.Should().Be("ADD TEST");
            resource.Description.Should().Be("Descrip");
            resource.Cost.Should().Be(151);
        }
    }
}
