namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReportExport : ContextBase
    {
        private IResult<IEnumerable<IEnumerable<string>>> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new AssemblyFailsDetailsReportRequestResource
                               {
                                   FromDate = 1.May(2020).ToString("o"),
                                   ToDate = 1.July(2020).ToString("o")
                               };
            this.ReportService.GetAssemblyFailsDetailsReportExport(1.May(2020), 1.July(2020))
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetAssemblyFailsDetailsReportExport(resource);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetAssemblyFailsDetailsReportExport(1.May(2020), 1.July(2020));
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<IEnumerable<string>>>>();
        }
    }
}