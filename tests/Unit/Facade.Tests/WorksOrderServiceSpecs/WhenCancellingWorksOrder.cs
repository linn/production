namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCancellingWorksOrder : ContextBase
    {
        private IResult<WorksOrder> result;

        private WorksOrderResource resource;

        private WorksOrder worksOrder;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 1234,
                                    CancelledBy = 33067,
                                    ReasonCancelled = "Reason"
                                };

            this.worksOrder = new WorksOrder
                                  {
                                      OrderNumber = 1234,
                                      PartNumber = "MAJIK"
                                  };

            this.WorksOrderRepository.FindById(this.resource.OrderNumber)
                .Returns(this.worksOrder);

            this.WorksOrderFactory
                .CancelWorksOrder(this.worksOrder, this.resource.CancelledBy, this.resource.ReasonCancelled)
                .Returns(new WorksOrder
                             {
                                 OrderNumber = 1234,
                                 PartNumber = "MAJIK",
                                 CancelledBy = this.resource.CancelledBy,
                                 ReasonCancelled = this.resource.ReasonCancelled,
                                 DateCancelled = new DateTime?()
                             });

            this.result = this.Sut.CancelWorksOrder(this.resource);
        }

        [Test]
        public void ShouldGetWorksOrder()
        {
            this.WorksOrderRepository.Received().FindById(this.resource.OrderNumber);
        }

        [Test]
        public void ShouldCallFactory()
        {
            this.WorksOrderFactory.Received().CancelWorksOrder(
                this.worksOrder,
                this.resource.CancelledBy,
                this.resource.ReasonCancelled);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<WorksOrder>>();
        }
    }
}