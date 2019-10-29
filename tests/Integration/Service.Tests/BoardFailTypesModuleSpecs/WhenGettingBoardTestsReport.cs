namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingBoardTestsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel { ReportTitle = new NameModel("title") };
            this.BoardTestReportFacadeService.GetBoardTestReport(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new SuccessResult<ResultsModel>(results));

            this.Response = this.Browser.Get(
                "/production/measures/board-tests-report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.July(2020).ToString("o"));
                        with.Query("toDate", 1.July(2021).ToString("o"));
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
            this.BoardTestReportFacadeService.Received().GetBoardTestReport(
                1.July(2020).ToString("o"),
                1.July(2021).ToString("o"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
