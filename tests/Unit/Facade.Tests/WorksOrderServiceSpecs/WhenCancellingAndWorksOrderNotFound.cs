namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCancellingAndWorksOrderNotFound : ContextBase
    {
        private IResult<WorksOrder> result;

        private WorksOrderResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 1234
                                };

            this.WorksOrderRepository.FindById(this.resource.OrderNumber)
                .Returns((WorksOrder)null);

            this.result = this.Sut.CancelWorksOrder(this.resource);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WorksOrderRepository.Received().FindById(this.resource.OrderNumber);
        }

        [Test]
        public void ShouldReturnNotFound()
        {
            this.result.Should().BeOfType<NotFoundResult<WorksOrder>>();
        }
    }
}