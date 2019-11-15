namespace Linn.Production.Service.Tests.AssemblyFailsReportsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Resources.RequestResources;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReportExport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AssemblyFailsReportsFacade.GetAssemblyFailsDetailsReportExport(Arg.Any<AssemblyFailsDetailsReportRequestResource>())
                .Returns(new SuccessResult<IEnumerable<IEnumerable<string>>>(new List<List<string>>()));

            this.Response = this.Browser.Get(
                "/production/reports/assembly-fails-details/report/export",
                with =>
                    {
                        with.Header("Accept", "text/csv");
                        with.Query("fromDate", 1.May(2020).ToString("o"));
                        with.Query("toDate", 1.July(2020).ToString("o"));
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
            this.AssemblyFailsReportsFacade.Received().GetAssemblyFailsDetailsReportExport(
                Arg.Is<AssemblyFailsDetailsReportRequestResource>(
                    r => r.FromDate == 1.May(2020).ToString("o") && r.ToDate == 1.July(2020).ToString("o")));
        }
    }
}
