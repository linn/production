namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMeasuresReportWithDefaultDates : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetAssemblyFailsMeasuresReport(null, null);
        }

        [Test]
        public void ShouldGetReportWithDefaultDates()
        {
            this.ReportService.Received().GetAssemblyFailsMeasuresReport(
                Arg.Is<DateTime>(a => a.Date == DateTime.UtcNow.Date.AddMonths(-4)),
                Arg.Is<DateTime>(b => b.Date == DateTime.UtcNow.Date),
                AssemblyFailGroupBy.boardPartNumber);
        }
    }
}