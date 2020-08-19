namespace Linn.Production.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var part1 = new Part { PartNumber = "P1", Description = "desc1" };
            var part2 = new Part { PartNumber = "P2", Description = "desc2" };

            this.PartsFacadeService.Search("P", Arg.Any<IEnumerable<string>>()).Returns(
                new SuccessResult<ResponseModel<IEnumerable<Part>>>(
                    new ResponseModel<IEnumerable<Part>>(new List<Part> { part1, part2 }, new List<string>())));


            this.Response = this.Browser.Get(
                "/production/maintenance/parts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "P");
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
            this.PartsFacadeService.Received().Search("P", Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartNumber == "P1");
            resource.Should().Contain(a => a.PartNumber == "P2");
        }
    }
}