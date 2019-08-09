namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrdersReport : ContextBase
    {
        private OutstandingWorksOrdersRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new OutstandingWorksOrdersRequestResource { ReportType = string.Empty, SearchParameter = string.Empty };
            var results = new ResultsModel(new[] { "col1" });
            this.OutstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport(Arg.Any<OutstandingWorksOrdersRequestResource>()).Returns(
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
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.OutstandingWorksOrdersReportFacade.Received().GetOutstandingWorksOrdersReport(Arg.Is<OutstandingWorksOrdersRequestResource>(r => r.ReportType == string.Empty));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
