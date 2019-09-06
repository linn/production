namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingWorksOrder : ContextBase
    {
        private WorksOrderResource resource;

        private IResult<WorksOrder> result;

        private WorksOrder worksOrder;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    PartNumber = "MAJIK",
                                    RaisedByDepartment = "DEPT",
                                    DocType = "DOC",
                                    RaisedBy = 33067,
                                    QuantityOutstanding = 3,
                                    KittedShort = "KIT"
                                };

            this.worksOrder = new WorksOrder { PartNumber = "MAJIK", RaisedByDepartment = "DEPT", RaisedBy = 33067 };

            this.WorksOrderFactory.RaiseWorksOrder(this.resource.PartNumber, this.resource.RaisedByDepartment, this.resource.RaisedBy)
                .Returns(this.worksOrder);

            this.result = this.Sut.AddWorksOrder(this.resource);
        }

        [Test]
        public void ShouldRaiseWorksOrder()
        {
            this.WorksOrderFactory.Received().RaiseWorksOrder(
                this.resource.PartNumber,
                this.resource.RaisedByDepartment,
                this.resource.RaisedBy);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WorksOrderRepository.Received().Add(this.worksOrder);
        }

        [Test]
        public void ShouldIssueSernos()
        {
            this.WorksOrderFactory.Received().IssueSerialNumber(
                this.resource.PartNumber,
                this.resource.OrderNumber,
                this.resource.DocType,
                this.resource.RaisedBy,
                this.resource.QuantityOutstanding);
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<WorksOrder>>();
            var dataResult = ((CreatedResult<WorksOrder>)this.result).Data;
            dataResult.PartNumber.Should().Be("MAJIK");
        }
    }
}
