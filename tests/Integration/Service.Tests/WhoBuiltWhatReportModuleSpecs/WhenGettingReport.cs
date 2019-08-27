namespace Linn.Production.Service.Tests.WhoBuiltWhatReportModuleSpecs
{
    using System.Collections.Generic;
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

    public class WhenGettingReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new List<ResultsModel> { new ResultsModel { ReportTitle = new NameModel("thing") } };
            this.WhoBuiltWhatReportFacadeService.WhoBuiltWhat(Arg.Any<string>(), Arg.Any<string>(), "S")
                .Returns(new SuccessResult<IEnumerable<ResultsModel>>(results));
            this.Response = this.Browser.Get(
                "/production/reports/who-built-what",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.May(2020).ToString("o"));
                        with.Query("toDate", 21.May(2020).ToString("o"));
                        with.Query("citCode", "S");
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
            this.WhoBuiltWhatReportFacadeService.Received().WhoBuiltWhat(
                Arg.Any<string>(),
                Arg.Any<string>(),
                "S");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("thing");
        }
    }
}