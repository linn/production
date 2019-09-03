namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingWorksOrder : ContextBase
    {
        private WorksOrderResource resource;

        private IResult<WorksOrder> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WorksOrderResource
                                {
                                    OrderNumber = 12345,
                                    DateRaised = DateTime.Now.ToString("o"),
                                    RaisedBy = 333,
                                    PartNumber = "part",
                                    QuantityBuilt = 0,
                                    QuantityOutstanding = 12
                                };

            this.PartsService.GetById("part").Returns(new SuccessResult<Part>(new Part { BomType = "b", AccountingCompany = "LINN"}));

            this.GetNextBatchService.GetNextBatch(this.resource.PartNumber).Returns(555);

            this.CanRaiseWorksOrderService.CanRaiseWorksOrder(this.resource.PartNumber).Returns("SUCCESS");

            this.result = this.Sut.AddWorksOrder(this.resource);
        }

        [Test]
        public void ShouldCallPartsService()
        {
            this.PartsService.Received().GetById(this.resource.PartNumber);
        }

        [Test]
        public void ShouldCallGetNextBatchService()
        {
            this.GetNextBatchService.Received().GetNextBatch(this.resource.PartNumber);
        }

        [Test]
        public void ShouldCallCanRaiseWorksOrderService()
        {
            this.CanRaiseWorksOrderService.Received().CanRaiseWorksOrder(this.resource.PartNumber);
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<WorksOrder>>();
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Should().BeOfType<CreatedResult<WorksOrder>>();
            var dataResult = ((CreatedResult<WorksOrder>)this.result).Data;
            dataResult.OrderNumber.Should().Be(12345);
        }
    }
}
