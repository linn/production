namespace Linn.Production.Service.Tests.PcasRevisionsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var pcasRevision1 = new PcasRevision { Cref = "001", PartNumber = "part1", PcasPartNumber = "pcas1" };
            var pcasRevision2 = new PcasRevision { Cref = "002", PartNumber = "part2", PcasPartNumber = "pcas2" };
            this.PcasRevisionService.Search("pcas")
                .Returns(new SuccessResult<IEnumerable<PcasRevision>>(new List<PcasRevision> { pcasRevision1, pcasRevision2 }));

            this.Response = this.Browser.Get(
                "/production/maintenance/pcas-revisions",
                with =>
                    {
                        with.Header("Accept", "application/json"); 
                        with.Query("searchTerm", "pcas");
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
            this.PcasRevisionService.Received().Search("pcas");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<PcasRevision>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.PartNumber == "part1");
            resources.Should().Contain(a => a.PartNumber == "part2");
        }
    }
}