namespace Linn.Production.Service.Tests.AssemblyFailsReportsModuleSpecs
{
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Production.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.AssemblyFailsReportsFacade.GetAssemblyFailsDetailsReport(Arg.Any<AssemblyFailsDetailsReportRequestResource>())
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
                "/production/reports/assembly-fails-details/report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", 1.May(2020).ToString("O"));
                        with.Query("toDate", 1.July(2020).ToString("O"));
                        with.Query("boardPartNumber", "BP1");
                        with.Query("circuitPartNumber", "CP");
                        with.Query("faultCode", "FC");
                        with.Query("citCode", "T");
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
            this.AssemblyFailsReportsFacade.Received().GetAssemblyFailsDetailsReport(
                Arg.Is<AssemblyFailsDetailsReportRequestResource>(
                    r => r.FromDate == 1.May(2020).ToString("O")
                         && r.ToDate == 1.July(2020).ToString("O")
                         && r.BoardPartNumber == "BP1"
                         && r.CircuitPartNumber == "CP"
                         && r.FaultCode == "FC"
                         && r.CitCode == "T"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
