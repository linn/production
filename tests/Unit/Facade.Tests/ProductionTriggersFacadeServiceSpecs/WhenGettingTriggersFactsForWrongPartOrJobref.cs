namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggersFactsForWrongPartOrJobref : ContextBase
    {
        private IResult<ProductionTriggerFacts> result;

        [SetUp]
        public void SetUp()
        {
            this.ProductionTriggerQueryRepository.FindBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns((ProductionTrigger)null);

            this.result = this.Sut.GetProductionTriggerFacts("AAAAAA", "SERIES K");
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.result.Should().BeOfType<NotFoundResult<ProductionTriggerFacts>>();
            this.result.As<NotFoundResult<ProductionTriggerFacts>>().Message.Should().Be("No facts found for that jobref and part number");
        }
    }
}