namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMeasuresReportWithBadDates : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetAssemblyFailsMeasuresReport(1.May(2020).ToString("O"), "Not a date", "part-number");
        }

        [Test]
        public void ShouldNotGetReport()
        {
            this.ReportService.DidNotReceive().GetAssemblyFailsMeasuresReport(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                AssemblyFailGroupBy.BoardPartNumber);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ResultsModel>>();
        }
    }
}