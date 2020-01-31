namespace Linn.Production.Service.Tests.ShortageModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
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
                OnesTwos = 4,
                Shortages = new List<ShortageResult>
                {
                    new ShortageResult { MetalworkShortage = false, ProcurementShortage = false, BoardShortage = true },
                    new ShortageResult { MetalworkShortage = false, ProcurementShortage = true, BoardShortage = false },
                    new ShortageResult { MetalworkShortage = true, ProcurementShortage = false, BoardShortage = false }
                }
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
            var report = this.Response.Body.DeserializeJson<ShortageSummaryReportResource>();
            report.ReportResults.Count().Should().Be(1);
            report.ReportResults.First()?.NumShortages.Should().Be(3);
            report.ReportResults.First()?.OnesTwos.Should().Be(4);
        }
    }
}
