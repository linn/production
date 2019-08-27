namespace Linn.Production.Service.Tests.WhoBuiltWhatReportModuleSpecs
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

    public class WhenGettingDetailsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel { ReportTitle = new NameModel("thing") };
            this.WhoBuiltWhatReportFacadeService.WhoBuiltWhatDetails(Arg.Any<string>(), Arg.Any<string>(), 1234)
                .Returns(new SuccessResult<ResultsModel>(results));
            this.Response = this.Browser.Get(
                "/production/reports/who-built-what-details",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.May(2020).ToString("o"));
                        with.Query("toDate", 21.May(2020).ToString("o"));
                        with.Query("userNumber", "1234");
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
            this.WhoBuiltWhatReportFacadeService.Received().WhoBuiltWhatDetails(
                Arg.Any<string>(),
                Arg.Any<string>(),
                1234);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("thing");
        }
    }
}