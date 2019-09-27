namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTriggerFactsWithOutstandingWorksOrders : ContextBase
    {
        private IResult<ProductionTriggerFacts> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTrigger
                              {
                                  PartNumber = "SERIES K",
                                  Description = "A serious product",
                                  Priority = "1",
                                  QtyBeingBuilt = 1
                              };

            this.ProductionTriggerQueryRepository.FindBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(trigger);

            var worksOrders = new List<WorksOrder>
                                  {
                                      new WorksOrder
                                          {
                                              OrderNumber = 800000,
                                              Quantity = 1,
                                              QuantityBuilt = 0,
                                              DateRaised = new DateTime(2019, 1, 1)
                                          }
                                  };

            this.WorksOrderRepository.FilterBy(Arg.Any < Expression<Func<WorksOrder, bool>>>())
                .Returns(worksOrders.AsQueryable());

            this.result = this.Sut.GetProductionTriggerFacts("CJCAIH", "SERIES K");
        }

        [Test]
        public void ShouldReturnSuccessRequestWithTriggerAndWorksOrders()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggerFacts>>();
            var facts = this.result.As<SuccessResult<ProductionTriggerFacts>>().Data;
            facts.Trigger.PartNumber.Should().Be("SERIES K");
            facts.OutstandingWorksOrders.Count().Should().Be(1);
        }

        [Test]
        public void ShouldNotCallWorksOrderRepository()
        {
            this.WorksOrderRepository.Received().FilterBy(Arg.Any<Expression<Func<WorksOrder, bool>>>());
        }
    }
}