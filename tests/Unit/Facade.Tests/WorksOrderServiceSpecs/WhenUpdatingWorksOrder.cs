namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingWorksOrder : ContextBase
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
                                    Quantity = 6,
                                    Links = new[] { new LinkResource("updated-by", $"/employees/{this.updatedBy}") }
                                };

            this.worksOrder = new WorksOrder { OrderNumber = 1234, PartNumber = "MAJIK" };

            this.WorksOrderRepository.FindById(this.resource.OrderNumber).Returns(this.worksOrder);

            this.result = this.Sut.UpdateWorksOrder(this.resource);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WorksOrderRepository.Received().FindById(this.resource.OrderNumber);
        }

        [Test]
        public void ShouldReturnNotFound()
        {
            this.result.Should().BeOfType<SuccessResult<WorksOrder>>();
        }
    }
}