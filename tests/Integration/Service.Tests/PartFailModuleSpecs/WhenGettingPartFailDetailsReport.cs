namespace Linn.Production.Service.Tests.PartFailModuleSpecs
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

    public class WhenGettingPartFailDetailsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });

            this.PartsReportFacadeService.GetPartFailDetailsReport(123, "fw", "tw", "et", "fc", "pn", "de").Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel { ReportTitle = new NameModel("title") }
                    });

            this.Response = this.Browser.Get(
                "/production/quality/part-fails/detail-report/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("supplierId", "123");
                        with.Query("fromWeek", "fw");
                        with.Query("toWeek", "tw");
                        with.Query("errorType", "et");
                        with.Query("faultCode", "fc");
                        with.Query("partNumber", "pn");
                        with.Query("department", "de");
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
            this.PartsReportFacadeService.Received().GetPartFailDetailsReport(123, "fw", "tw", "et", "fc", "pn", "de");
        }

        [Test]
        public void ShouldReturnReport()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
