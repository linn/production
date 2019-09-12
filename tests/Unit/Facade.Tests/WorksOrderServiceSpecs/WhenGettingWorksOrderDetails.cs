namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenGettingWorksOrderDetails : ContextBase
    {
        private IResult<WorksOrderDetails> result;

        private string partNumber;

        private WorksOrderDetails worksOrderDetails;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";

            this.worksOrderDetails = new WorksOrderDetails
                                         {
                                             AuditDisclaimer = "Disclaimer",
                                             PartNumber = this.partNumber,
                                             PartDescription = "Description",
                                             WorkStationCode = "Code"
                                         };

            this.WorksOrderFactory.GetWorksOrderDetails(this.partNumber).Returns(this.worksOrderDetails);

            this.result = this.Sut.GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldCallWorksOrderFactory()
        {
            this.WorksOrderFactory.Received().GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<WorksOrderDetails>>();
            var dataResult = ((SuccessResult<WorksOrderDetails>)this.result).Data;
            dataResult.PartNumber.Should().Be(this.partNumber);
            dataResult.AuditDisclaimer.Should().Be("Disclaimer");
        }
    }

    public class WhenGettingWorksOrderDetailsForIncorrectPart : ContextBase
    {
        private IResult<WorksOrderDetails> result;

        private string partNumber;

        private WorksOrderDetails worksOrderDetails;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";

            this.worksOrderDetails = new WorksOrderDetails
                                         {
                                             AuditDisclaimer = "Disclaimer",
                                             PartNumber = this.partNumber,
                                             PartDescription = "Description",
                                             WorkStationCode = "Code"
                                         };

            this.WorksOrderFactory.GetWorksOrderDetails(this.partNumber).Throws(new DomainException("Exception"));

            this.result = this.Sut.GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldCallWorksOrderFactory()
        {
            this.WorksOrderFactory.Received().GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrderDetails>>();
        }
    }
}