namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrdersReportCsv : ContextBase
    {
        private OutstandingWorksOrdersRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new OutstandingWorksOrdersRequestResource { ReportType = string.Empty, SearchParameter = string.Empty };
            this.OutstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv(Arg.Any<OutstandingWorksOrdersRequestResource>())
                .Returns(new SuccessResult<IEnumerable<IEnumerable<string>>>(new List<List<string>>()));

            this.Response = this.Browser.Get(
                "/production/maintenance/works-orders/outstanding-works-orders-report/export",
                with =>
                    {
                        with.Header("Accept", "text/csv");
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
            this.OutstandingWorksOrdersReportFacade.Received().GetOutstandingWorksOrdersReportCsv(Arg.Is<OutstandingWorksOrdersRequestResource>(r => r.ReportType == string.Empty));
        }
    }
}
