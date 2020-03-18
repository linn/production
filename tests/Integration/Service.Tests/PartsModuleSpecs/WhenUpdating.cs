namespace Linn.Production.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PartResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartResource { PartNumber = "PART", Description = "DESC", LibraryRef = "LIB" };

            var part = new Part { PartNumber = "PART", Description = "DESC", LibraryRef = "LIB" };

            this.PartsFacadeService.Update("PART", Arg.Any<PartResource>()).Returns(new SuccessResult<Part>(part));

            this.Response = this.Browser.Put(
                "production/maintenance/parts/PART",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.PartsFacadeService.Received().Update("PART", Arg.Is<PartResource>(p => p.PartNumber == "PART"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartResource>();
            resource.PartNumber.Should().Be("PART");
        }
    }
}