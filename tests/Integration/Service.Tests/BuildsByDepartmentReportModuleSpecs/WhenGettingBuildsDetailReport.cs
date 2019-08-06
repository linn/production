namespace Linn.Production.Service.Tests.BuildsByDepartmentReportModuleSpecs
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

    public class WhenGettingBuildsDetailReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.BuildsByDepartmentReportFacadeService.GetBuildsDetailReport(
                    DateTime.UnixEpoch,
                    DateTime.UnixEpoch,
                    "001",
                    "Value",
                    false)
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
                "/production/reports/builds-detail",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Accept", "application/json");
                    with.Query("fromDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("toDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("monthly", "false");
                    with.Query("quantityOrValue", "Value");
                    with.Query("department", "001");
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
            this.BuildsByDepartmentReportFacadeService.Received().GetBuildsDetailReport(
                DateTime.UnixEpoch,
                DateTime.UnixEpoch,
                "001",
                "Value",
                false);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
