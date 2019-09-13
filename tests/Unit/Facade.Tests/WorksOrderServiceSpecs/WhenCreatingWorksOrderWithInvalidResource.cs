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
                                    Quantity = 3
                                };

            this.worksOrder = new WorksOrder
                                  {
                                      PartNumber = this.resource.PartNumber,
                                      RaisedByDepartment = this.resource.RaisedByDepartment,
                                      DocType = this.resource.DocType,
                                      RaisedBy = this.resource.RaisedBy,
                                      Quantity = this.resource.Quantity
                                  };

            this.WorksOrderFactory.RaiseWorksOrder(Arg.Any<WorksOrder>())
                .Throws(new DomainException("Exception"));

            this.result = this.Sut.AddWorksOrder(this.resource);
        }

        [Test]
        public void ShouldRaiseWorksOrder()
        {
            this.WorksOrderFactory.Received().RaiseWorksOrder(Arg.Any<WorksOrder>());
        }

        [Test]
        public void ShouldNotCallRepository()
        {
            this.WorksOrderRepository.DidNotReceive().Add(this.worksOrder);
        }

        [Test]
        public void ShouldNotIssueSernos()
        {
            this.WorksOrderUtilities.DidNotReceive().IssueSerialNumber(
                this.resource.PartNumber,
                this.resource.OrderNumber,
                this.resource.DocType,
                this.resource.RaisedBy,
                this.resource.Quantity);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrder>>();
        }
    }
}