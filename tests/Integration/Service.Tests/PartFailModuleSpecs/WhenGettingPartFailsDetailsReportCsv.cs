namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartFailsDetailsReportCsv : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PartsReportFacadeService.GetPartFailDetailsReportCsv(123, "fw", "tw", "et", "fc", "pn", "de").Returns(
                new SuccessResult<IEnumerable<IEnumerable<string>>>(new List<List<string>>()));

            this.Response = this.Browser.Get(
                "/production/quality/part-fails/detail-report/report/export",
                with =>
                    {
                        with.Header("Accept", "text/csv");
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
            this.PartsReportFacadeService.Received().GetPartFailDetailsReportCsv(123, "fw", "tw", "et", "fc", "pn", "de");
        }
    }
}