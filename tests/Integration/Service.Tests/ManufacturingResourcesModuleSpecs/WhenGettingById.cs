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

    public class WhenGettingManufacturingResource : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new ManufacturingResource("TESTCODE", "desc", 155);
            this.ManufacturingResourceService.GetById("TESTCODE")
                .Returns(new SuccessResult<ManufacturingResource>(resource));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-resources/TESTCODE",
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
            this.ManufacturingResourceService.Received().GetById("TESTCODE");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingResourceResource>();
            resource.ResourceCode.Should().Be("TESTCODE");
            resource.Description.Should().Be("desc");
            resource.Cost.Should().Be(155);
        }
    }
}
