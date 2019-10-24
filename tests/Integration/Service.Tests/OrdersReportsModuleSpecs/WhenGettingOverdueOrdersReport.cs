namespace Linn.Production.Service.Tests.OrdersReportsModuleSpecs
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

    public class WhenGettingOverdueOrdersReport : ContextBase
    {
        private string testDate;

        [SetUp]
        public void SetUp()
        {
            this.testDate = DateTime.UnixEpoch.ToString("d");

            var results = new ResultsModel(new[] { "col1" });
            this.OrdersReportsFacadeService
                .GetOverdueOrdersReport("RB", "DM")
                .Returns(
                    new SuccessResult<ResultsModel>(results)
                        {
                            Data = new ResultsModel { ReportTitle = new NameModel("title") }
                        });

            this.Response = this.Browser.Get(
                "/production/reports/overdue-orders/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("jobId", "123");
                        with.Query("fromDate", this.testDate);
                        with.Query("toDate", this.testDate);
                        with.Query("accountingCompany", "AC");
                        with.Query("stockPool", "SP");
                        with.Query("reportBy", "RB");
                        with.Query("daysMethod", "DM");
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
            this.OrdersReportsFacadeService.Received().GetOverdueOrdersReport("RB", "DM");
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
