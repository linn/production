namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Triggers;

    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggerFactsWithBackOrders : ContextBase
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
                                  ReqtForSalesOrdersBE = 1
                              };

            this.ProductionTriggerQueryRepository.FindBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(trigger);

            var productionBackOrders = new List<ProductionBackOrder>
            {
                new ProductionBackOrder
                    {
                        OrderNumber = 300000,
                        OrderLine = 1,
                        ArticleNumber = "SERIES K",
                        JobId = 1,
                        CitCode = "S",
                        BackOrderQty = 1,
                        RequestedDeliveryDate = DateTime.Now.Date,
                        BaseValue = 100
                    }
            };

            var accountingCompany = new AccountingCompany
                                        {
                                            Name = "LINN",
                                            Description = "Linn Products Ltd",
                                            LatestSosJobId = 1,
                                            DateLatestSosJobId = DateTime.Now.Date
                                        };

            this.AccountingCompanyRepository.FindById(Arg.Any<string>()).Returns(accountingCompany);
            this.ProductionBackOrderQueryRepository.FilterBy(Arg.Any<Expression<Func<ProductionBackOrder, bool>>>())
                .Returns(productionBackOrders.AsQueryable());

            this.result = this.Sut.GetProductionTriggerFacts("CJCAIH", "SERIES K");
        }

        [Test]
        public void ShouldReturnSuccessRequestWithTriggerAndWorksOrders()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggerFacts>>();
            var facts = this.result.As<SuccessResult<ProductionTriggerFacts>>().Data;
            facts.Trigger.PartNumber.Should().Be("SERIES K");
            facts.OutstandingSalesOrders.Count().Should().Be(1);
        }

        [Test]
        public void ShouldNotCallWorksOrderRepository()
        {
            this.ProductionBackOrderQueryRepository.Received().FilterBy(Arg.Any<Expression<Func<ProductionBackOrder, bool>>>());
        }
    }
}