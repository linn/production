namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenCreatingWorksOrderWithInvalidResource : ContextBase
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

            this.worksOrder = new WorksOrder { PartNumber = "MAJIK", RaisedByDepartment = "DEPT" };

            this.WorksOrderFactory.RaiseWorksOrder(this.resource.PartNumber, this.resource.RaisedByDepartment, this.resource.RaisedBy)
                .Throws(new DomainException("Exception"));

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
        public void ShouldNotCallRepository()
        {
            this.WorksOrderRepository.DidNotReceive().Add(this.worksOrder);
        }

        [Test]
        public void ShouldNotIssueSernos()
        {
            this.WorksOrderFactory.DidNotReceive().IssueSerialNumber(
                this.resource.PartNumber,
                this.resource.OrderNumber,
                this.resource.DocType,
                this.resource.RaisedBy,
                this.resource.QuantityOutstanding);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrder>>();
        }
    }
}