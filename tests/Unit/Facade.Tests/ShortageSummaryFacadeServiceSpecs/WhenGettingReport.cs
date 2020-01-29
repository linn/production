namespace Linn.Production.Facade.Tests.ShortageSummaryFacadeServiceSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private IResult<ShortageSummary> result;

        [SetUp]
        public void SetUp()
        {
            var summary = new ShortageSummary
            {
                OnesTwos = 5,
                Shortages = new List<ShortageResult>()
                {
                    new ShortageResult { MetalworkShortage = false, ProcurementShortage = false, BoardShortage = true },
                    new ShortageResult { MetalworkShortage = false, ProcurementShortage = true, BoardShortage = false },
                    new ShortageResult { MetalworkShortage = true, ProcurementShortage = false, BoardShortage = false }
                }
            };

            this.ShortageSummaryReportService.ShortageSummaryByCit("S","AAAAAA").Returns(summary);
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
            dataResult.NumShortages().Should().Be(3);
        }
    }
}
