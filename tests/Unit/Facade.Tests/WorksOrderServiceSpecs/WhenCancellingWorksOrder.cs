namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCancellingWorksOrder : ContextBase
    {
        private IResult<WorksOrder> result;

        private WorksOrderResource resource;

        private WorksOrder worksOrder;

        private int updatedBy;

        [SetUp]
        public void SetUp()
        {
            this.updatedBy = 33067;

            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 1234,
                                    ReasonCancelled = "Reason",
                                    CancelledBy = 33067,
                                    Links = new[] { new LinkResource("updated-by", $"/employees/{this.updatedBy}") }
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
            this.result.Should().BeOfType<SuccessResult<WorksOrder>>();
            var dataResult = this.result.As<SuccessResult<WorksOrder>>().Data;
            dataResult.CancelledBy.Should().Be(this.updatedBy);
        }
    }
}