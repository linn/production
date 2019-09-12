namespace Linn.Production.Service.Tests.SmtReportsModuleSpecs
{
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrderPartsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.SmtReportsFacade.GetPartsForOutstandingWorksOrders(Arg.Any<string>(), Arg.Any<string[]>())
                .Returns(
                    new SuccessResult<ResultsModel>(results)
                        {
                            Data = new ResultsModel
                                       {
                                           ReportTitle = new NameModel("title")
                                       }
                        });

            this.Response = this.Browser.Get(
                "/production/reports/smt/outstanding-works-order-parts/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.May(2020).ToString("O"));
                        with.Query("smtLine", "SMT1");
                        with.Query("parts", "P1");
                        with.Query("parts", "P2");
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
            this.SmtReportsFacade.Received().GetPartsForOutstandingWorksOrders(
                Arg.Is<string>(s => s == "SMT1"),
                Arg.Is<string[]>(s => s.Contains("P1") && s.Contains("P2") && s.Length == 2));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}