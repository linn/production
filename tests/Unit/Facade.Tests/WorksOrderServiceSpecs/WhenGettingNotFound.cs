namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByPartNumberAndNotFound : ContextBase
    {
        private IResult<WorksOrder> result;

        private int orderNumber;

        [SetUp]
        public void SetUp()
        {
            this.orderNumber = 123;
            this.WorksOrderRepository.FindById(this.orderNumber)
                .Returns((WorksOrder)null);
            this.result = this.Sut.GetById(this.orderNumber);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WorksOrderRepository.Received().FindById(this.orderNumber);
        }

        [Test]
        public void ShouldReturnNotFound()
        {
            this.result.Should().BeOfType<NotFoundResult<WorksOrder>>();
        }
    }
}