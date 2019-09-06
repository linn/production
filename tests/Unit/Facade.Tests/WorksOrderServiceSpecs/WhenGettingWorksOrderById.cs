namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWorksOrderById : ContextBase
    {
        private IResult<WorksOrder> result;

        private int orderNumber;

        [SetUp]
        public void SetUp()
        {
            this.orderNumber = 123;
            this.WorksOrderRepository.FindById(this.orderNumber)
                .Returns(new WorksOrder { OrderNumber = this.orderNumber });
            this.result = this.Sut.GetWorksOrder(this.orderNumber);
        }

        [Test]
        public void ShouldGetWorksOrder()
        {
            this.WorksOrderRepository.Received().FindById(this.orderNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<WorksOrder>>();
            var dataResult = ((SuccessResult<WorksOrder>)this.result).Data;
            dataResult.OrderNumber.Should().Be(this.orderNumber);
        }
    }
}
