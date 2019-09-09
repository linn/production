namespace Linn.Production.Service.Tests.AssemblyFailsReportsModuleSpecs
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

    public class WhenGettingMeasuresReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.AssemblyFailsReportsFacade.GetAssemblyFailsMeasuresReport(
                    1.May(2020).ToString("O"),
                    1.July(2020).ToString("O"),
                    "part-number")
                .Returns(
                    new SuccessResult<ResultsModel>(results)
                        {
                            Data = new ResultsModel
                                       {
                                           ReportTitle =
                                               new NameModel("title")
                                       }
                        });

            this.Response = this.Browser.Get(
                "/production/reports/assembly-fails-measures",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.May(2020).ToString("O"));
                        with.Query("toDate", 1.July(2020).ToString("O"));
                        with.Query("groupBy", "part-number");
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
            this.AssemblyFailsReportsFacade.Received().GetAssemblyFailsMeasuresReport(
                1.May(2020).ToString("O"),
                1.July(2020).ToString("O"),
                "part-number");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}