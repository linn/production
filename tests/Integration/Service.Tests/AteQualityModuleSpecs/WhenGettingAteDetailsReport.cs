namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAteDetailsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var report = new ResultsModel { ReportTitle = new NameModel("Title") };
            this.AteReportsFacadeService.GetDetailsReport(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .Returns(new SuccessResult<ResultsModel>(report));

            this.Response = this.Browser.Get(
                "/production/reports/ate/details/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", "isoDate1");
                        with.Query("toDate", "isoDate2");
                        with.Query("smtOrPcb", "smt");
                        with.Query("placeFound", "somewhere");
                        with.Query("board", "board");
                        with.Query("component", "component");
                        with.Query("faultCode", "fault code");
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
            this.AteReportsFacadeService.Received().GetDetailsReport(
                "isoDate1",
                "isoDate2",
                "smt",
                "somewhere",
                "board",
                "component",
                "fault code");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("Title");
        }
    }
}