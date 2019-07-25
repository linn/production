namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
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

    public class WhenGettingOutstandingWorksOrdersReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.OutstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport().Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel
                                   {
                                       ReportTitle =
                                           new NameModel("title")
                                   }
                    });

            this.Response = this.Browser.Get(
                "/production/maintenance/works-orders/outstanding-works-orders-report",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.OutstandingWorksOrdersReportFacade.Received().GetOutstandingWorksOrdersReport();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
