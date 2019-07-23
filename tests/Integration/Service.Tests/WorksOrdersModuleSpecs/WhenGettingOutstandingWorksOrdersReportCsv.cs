namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrdersReportCsv : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.OutstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv()
                .Returns(new SuccessResult<IEnumerable<IEnumerable<string>>>(new List<List<string>>()));

            this.Response = this.Browser.Get(
                "/production/maintenance/works-orders/outstanding-works-orders-report/export",
                with => { with.Header("Accept", "text/csv"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.OutstandingWorksOrdersReportFacade.Received().GetOutstandingWorksOrdersReportCsv();
        }
    }
}
