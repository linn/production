namespace Linn.Production.Service.Tests.ShortageModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var shortageSummary = new ShortageSummary
            {
                NumShortages = 1,
                OnesTwos = 2,
                Metalwork = 0,
                BAT = 1,
                Procurement = 0,
                Shortages = new List<ShortageResult>()
            };

            this.ShortageSummaryFacadeService.ShortageSummaryByCit("S","AAAAAA")
                .Returns(new SuccessResult<ShortageSummary>(shortageSummary));

            this.Response = this.Browser.Get(
                "/production/reports/shortages",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("citCode", "S");
                    with.Query("ptlJobref", "AAAAAA");
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
            this.ShortageSummaryFacadeService.Received().ShortageSummaryByCit("S","AAAAAA");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ShortageSummaryResource>();
            resource.NumShortages.Should().Be(1);
            resource.OnesTwos.Should().Be(2);
        }
    }
}
