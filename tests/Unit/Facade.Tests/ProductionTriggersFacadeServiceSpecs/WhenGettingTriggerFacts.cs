namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggerFacts : ContextBase
    {
        private IResult<ProductionTriggerFacts> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTrigger
                              {
                                  PartNumber = "SERIES K", Description = "A serious product", Priority = "1", QtyBeingBuilt = 0
                              };

            this.ProductionTriggerQueryRepository.FindBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(trigger);

            this.result = this.Sut.GetProductionTriggerFacts("CJCAIH", "SERIES K");
        }

        [Test]
        public void ShouldReturnSuccessRequestWithTrigger()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggerFacts>>();
            var facts = this.result.As<SuccessResult<ProductionTriggerFacts>>().Data;
            facts.Trigger.PartNumber.Should().Be("SERIES K");
        }

        [Test]
        public void ShouldNotCallWorksOrderRepository()
        {

            this.WorksOrderRepository.DidNotReceive().FilterBy(Arg.Any<Expression<Func<WorksOrder, bool>>>());
        }
    }
}