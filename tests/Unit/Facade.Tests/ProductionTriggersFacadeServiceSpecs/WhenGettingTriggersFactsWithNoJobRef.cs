namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NUnit.Framework;

    public class WhenGettingTriggersFactsWithNoJobRef : ContextBase
    {
        private IResult<ProductionTriggerFacts> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetProductionTriggerFacts(string.Empty, "SERIES K");
        }

        [Test]
        public void ShouldReturnBadRequestResult()
        {
            this.result.Should().BeOfType<BadRequestResult<ProductionTriggerFacts>>();
            this.result.As<BadRequestResult<ProductionTriggerFacts>>().Message.Should().Be("Must supply a jobref");
        }
    }
}