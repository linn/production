namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAndWorksOrderNotFound : ContextBase
    {
        private IResult<WorksOrder> result;

        private WorksOrderResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 1234,
                                    Links = new[] { new LinkResource("updated-by", $"/employees/33067") }
                                };

            this.WorksOrderRepository.FindById(this.resource.OrderNumber).Returns((WorksOrder)null);

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
            this.result.Should().BeOfType<NotFoundResult<WorksOrder>>();
        }
    }
}