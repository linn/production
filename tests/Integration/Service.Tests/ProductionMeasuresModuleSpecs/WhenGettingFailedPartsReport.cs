namespace Linn.Production.Service.Tests.ProductionMeasuresModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFailedPartsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ProductionMeasuresReportFacade.GetFailedPartsReport("S", null, null, false, null)
                .Returns(new SuccessResult<IEnumerable<ResultsModel>>(new List<ResultsModel>
                                                                          {
                                                                              new ResultsModel { ReportTitle = new NameModel("Title") }
                                                                          }));

            this.Response = this.Browser.Get(
                "/production/reports/failed-parts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("citCode", "S");
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
            this.ProductionMeasuresReportFacade.Received().GetFailedPartsReport("S", null, null, false, null);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("Title");
        }
    }
}
