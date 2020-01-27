namespace Linn.Production.Facade.Tests.ShortageSummaryFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private IResult<ShortageSummary> result;

        [SetUp]
        public void SetUp()
        {
            this.ShortageSummaryReportService.ShortageSummaryByCit("S","AAAAAA").Returns(new ShortageSummary { NumShortages = 2, OnesTwos = 5, BAT = 1, Metalwork = 0, Procurement = 1});
            this.result = this.Sut.ShortageSummaryByCit("S", "AAAAAA");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ShortageSummaryReportService.Received().ShortageSummaryByCit("S", "AAAAAA");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ShortageSummary>>();
            var dataResult = ((SuccessResult<ShortageSummary>)this.result).Data;
            dataResult.NumShortages.Should().Be(2);
        }
    }
}
