namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggersWithNoCit : ContextBase
    {
        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetProductionTriggerReport("CJCAIH", string.Empty, "Full");
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ProductionTriggersReport>>();
            this.result.As<BadRequestResult<ProductionTriggersReport>>().Message.Should().Be("You must supply a citCode");
        }
    }
}