namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenCancellingWorksOrderWithInvalidParameters : ContextBase
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
                                    ReasonCancelled = string.Empty
                                };

            this.worksOrder = new WorksOrder
                                  {
                                      OrderNumber = 1234,
                                      PartNumber = "MAJIK"
                                  };

            this.WorksOrderRepository.FindById(this.resource.OrderNumber)
                .Returns(this.worksOrder);

            this.result = this.Sut.UpdateWorksOrder(this.resource);
        }

        [Test]
        public void ShouldGetWorksOrder()
        {
            this.WorksOrderRepository.Received().FindById(this.resource.OrderNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrder>>();
        }
    }
}