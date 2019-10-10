namespace Linn.Production.Service.Tests.OrdersReportsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMCDReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ManufacturingCommitDateResults
                              {
                                  Results = new List<ManufacturingCommitDateResult>
                                                {
                                                    new ManufacturingCommitDateResult
                                                        {
                                                            NumberOfLines = 34,
                                                            ProductType = "Excellent",
                                                            Results = new ResultsModel { ReportTitle = new NameModel("details") }
                                                        }
                                                },
                                  IncompleteLinesAnalysis = new ResultsModel { ReportTitle = new NameModel("title") }
                              };
            this.OrdersReportsFacadeService.ManufacturingCommitDateReport(Arg.Any<string>())
                .Returns(new SuccessResult<ManufacturingCommitDateResults>(results));
            this.Response = this.Browser.Get(
                "/production/reports/manufacturing-commit-date/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("date", 1.May(2020).ToString("o"));
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
            this.OrdersReportsFacadeService.Received().ManufacturingCommitDateReport(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingCommitDateResultsResource>();
            resource.IncompleteLinesAnalysis.ReportResults.First().title.displayString.Should().Be("title");
            resource.Results.Should().HaveCount(1);
            resource.Results.First().NumberOfLines.Should().Be(34);
            resource.Results.First().ProductType.Should().Be("Excellent");
            resource.Results.First().Results.ReportResults.First().title.displayString.Should().Be("details");
        }
    }
}