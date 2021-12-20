namespace Linn.Production.Service.Tests.ManufacturingTimingsReportModuleSpecs
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTimingsdReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.Service.GetManufacturingTimingsReport(
                    DateTime.UnixEpoch,
                    DateTime.UnixEpoch, Arg.Any<string>())
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
               "/production/reports/manufacturing-timings",
               with =>
               {
                   with.Header("Accept", "application/json");
                   with.Header("Accept", "application/json");
                   with.Query("startDate", DateTime.UnixEpoch.ToString("d"));
                   with.Query("endDate", DateTime.UnixEpoch.ToString("d"));
                   with.Query("citCode", "K");
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
            this.Service.Received().GetManufacturingTimingsReport(
                DateTime.UnixEpoch,
                DateTime.UnixEpoch, Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
