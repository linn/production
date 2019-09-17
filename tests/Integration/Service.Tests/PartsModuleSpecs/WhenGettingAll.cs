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

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var part1 = new Part { PartNumber = "1", Description = "desc1" };
            var part2 = new Part { PartNumber = "2", Description = "desc2" };

            this.PartFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Part>>(new List<Part> { part1, part2 }));


            this.Response = this.Browser.Get(
                "/production/maintenance/parts",
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
            this.PartFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartNumber == "1");
            resource.Should().Contain(a => a.PartNumber == "2");
        }
    }
}