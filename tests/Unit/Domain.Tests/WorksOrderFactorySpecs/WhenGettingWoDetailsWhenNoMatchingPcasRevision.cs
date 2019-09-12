namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWoDetailsWhenNoMatchingPcasRevision : ContextBase
    {
        private string partNumber;

        private string partDescription;

        private WorksOrderDetails result;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";
            this.partDescription = "DESCRIPTION";

            this.PartsRepository.FindById(this.partNumber)
                .Returns(new Part { PartNumber = this.partNumber, Description = this.partDescription });

            this.PcasRevisionsRepository.FindBy(Arg.Any<Expression<Func<PcasRevision, bool>>>()).Returns((PcasRevision)null);

            this.result = this.Sut.GetWorksOrderDetails(partNumber);
        }

        [Test]
        public void ShouldCallPartsRepository()
        {
            this.PartsRepository.Received().FindById(this.partNumber);
        }

        [Test]
        public void ShouldCallPcasRevisionsRepository()
        {
            this.PcasRevisionsRepository.Received().FindBy(Arg.Any<Expression<Func<PcasRevision, bool>>>());
        }

        [Test]
        public void ShouldReturnNull()
        {
            this.result.AuditDisclaimer.Should().BeNullOrEmpty();
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.PartDescription.Should().Be(this.partDescription);
            this.result.PartNumber.Should().Be(this.partNumber);
        }
    }
}
