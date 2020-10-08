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

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var part = new Part { PartNumber = "PART", Description = "DESC" };

            this.PartsFacadeService.GetById("PART", Arg.Any<IEnumerable<string>>()).Returns(
                new SuccessResult<ResponseModel<Part>>(new ResponseModel<Part>(part, new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/parts/PART",
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
            this.PartsFacadeService.Received().GetById("PART", Arg.Any<IEnumerable<string>>());
        }

        [Test]

        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartResource>();
            resource.PartNumber.Should().Be("PART");
            resource.Description.Should().Be("DESC");
        }
    }
}