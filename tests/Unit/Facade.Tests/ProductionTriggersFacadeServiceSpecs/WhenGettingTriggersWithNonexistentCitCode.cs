namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggersWithNonexistentCitCode : ContextBase
    {
        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            this.PtlMasterRepository.GetRecord().Returns(new PtlMaster { LastFullRunJobref = "AAAAAA" });
            this.result = this.Sut.GetProductionTriggerReport("CJCAIH", "A");
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<NotFoundResult<ProductionTriggersReport>>();
            this.result.As<NotFoundResult<ProductionTriggersReport>>().Message.Should().Be("cit A not found");
        }
    }
}