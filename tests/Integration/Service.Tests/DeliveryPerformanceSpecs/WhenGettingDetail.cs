namespace Linn.Production.Service.Tests.DeliveryPerformanceSpecs
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

    public class WhenGettingDetail : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.DeliveryPerfResultFacadeService.GetDelPerfDetail(
                    "S", 1)
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
                "/production/reports/delperf/details",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("citCode", "S");
                    with.Query("priority", "1");
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
            this.DeliveryPerfResultFacadeService.Received().GetDelPerfDetail(
                "S", 1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}