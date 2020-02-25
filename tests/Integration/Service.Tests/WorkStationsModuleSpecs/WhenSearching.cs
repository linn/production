namespace Linn.Production.Service.Tests.WorkStationsModuleSpecs
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
            var a = new WorkStation
                        {
                            WorkStationCode = "wscd1",
                            Description = "d",
                            CitCode = "citcode1",
                            AlternativeWorkStationCode = "2",
                            VaxWorkStation = "vax?",
                            ZoneType = "smt1"
                        };
            var b = new WorkStation
                        {
                            WorkStationCode = "wscd2",
                            Description = "d",
                            CitCode = "citcode1",
                            AlternativeWorkStationCode = "2",
                            VaxWorkStation = "vax?",
                            ZoneType = "smt1"
                        };

            var workStations = new List<WorkStation> { a, b };

            this.WorkStationService.Search("citcode1")
                .Returns(new SuccessResult<IEnumerable<WorkStation>>(workStations));

            this.Response = this.Browser.Get(
                "/production/maintenance/work-stations",
                with => { with.Header("Accept", "application/json"); with.Query("searchTerm", "citcode1"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorkStationService.Received().Search("citcode1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<WorkStationResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.All(a => a.CitCode == "citcode1").Should().BeTrue();
            resources.Should().Contain(a => a.WorkStationCode == "wscd1");
            resources.Should().Contain(a => a.WorkStationCode == "wscd2");
        }
    }
}
