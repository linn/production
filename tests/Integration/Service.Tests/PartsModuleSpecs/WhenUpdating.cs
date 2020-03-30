namespace Linn.Production.Service.Tests.PartsModuleSpecs
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

    public class WhenUpdating : ContextBase
    {
        private PartResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartResource { PartNumber = "PART", LibraryName = "LIB" };

            var part = new Part { PartNumber = "PART", LibraryName = "LIB" };

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.PartUpdate, Arg.Any<List<string>>())
                .Returns(true);

            this.PartsFacadeService.Update("PART", Arg.Any<PartResource>(), Arg.Any<List<string>>()).Returns(
                new SuccessResult<ResponseModel<Part>>(new ResponseModel<Part>(part, new List<string>())));

            this.Response = this.Browser.Put(
                "/production/maintenance/parts/PART",
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
            this.PartsFacadeService.Received().Update("PART", Arg.Any<PartResource>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartResource>();
            resource.PartNumber.Should().Be("PART");
            resource.LibraryName.Should().Be("LIB");
        }
    }
}