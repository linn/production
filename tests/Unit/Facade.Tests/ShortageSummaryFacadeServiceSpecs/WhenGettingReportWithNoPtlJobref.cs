namespace Linn.Production.Facade.Tests.ShortageSummaryFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using NUnit.Framework;

    public class WhenGettingReportWithNoPtlJobref : ContextBase
    {
        private IResult<ShortageSummary> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.ShortageSummaryByCit("S", string.Empty);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ShortageSummary>>();
            ((BadRequestResult<ShortageSummary>)this.result).Message.Should().Be("Must specify a PTL Jobref");
        }
    }
}