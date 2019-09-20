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
        private IResult<WorksOrderPartDetails> result;

        private string partNumber;

        private WorksOrderPartDetails worksOrderPartDetails;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";

            this.worksOrderPartDetails = new WorksOrderPartDetails
                                         {
                                             AuditDisclaimer = "Disclaimer",
                                             PartNumber = this.partNumber,
                                             PartDescription = "Description",
                                             WorkStationCode = "Code"
                                         };

            this.WorksOrderUtilities.GetWorksOrderDetails(this.partNumber).Returns(this.worksOrderPartDetails);

            this.result = this.Sut.GetWorksOrderPartDetails(this.partNumber);
        }

        [Test]
        public void ShouldCallWorksOrderFactory()
        {
            this.WorksOrderUtilities.Received().GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<WorksOrderPartDetails>>();
            var dataResult = ((SuccessResult<WorksOrderPartDetails>)this.result).Data;
            dataResult.PartNumber.Should().Be(this.partNumber);
            dataResult.AuditDisclaimer.Should().Be("Disclaimer");
        }
    }

    public class WhenGettingWorksOrderDetailsForIncorrectPart : ContextBase
    {
        private IResult<WorksOrderPartDetails> result;

        private string partNumber;

        private WorksOrderPartDetails worksOrderPartDetails;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";

            this.worksOrderPartDetails = new WorksOrderPartDetails
                                         {
                                             AuditDisclaimer = "Disclaimer",
                                             PartNumber = this.partNumber,
                                             PartDescription = "Description",
                                             WorkStationCode = "Code"
                                         };

            this.WorksOrderUtilities.GetWorksOrderDetails(this.partNumber).Throws(new DomainException("Exception"));

            this.result = this.Sut.GetWorksOrderPartDetails(this.partNumber);
        }

        [Test]
        public void ShouldCallWorksOrderFactory()
        {
            this.WorksOrderUtilities.Received().GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<WorksOrderPartDetails>>();
        }
    }
}