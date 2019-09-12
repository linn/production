namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWoDetailsWhenBoardForAuditAndForCutClinch : ContextBase
    {
        private string partNumber;

        private string partDescription;

        private string boardCode;

        private WorksOrderDetails result;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";
            this.partDescription = "DESCRIPTION";
            this.boardCode = "123AB";

            this.PartsRepository.FindById(this.partNumber)
                .Returns(new Part { PartNumber = this.partNumber, Description = this.partDescription });

            this.PcasRevisionsRepository.FindBy(Arg.Any<Expression<Func<PcasRevision, bool>>>()).Returns(
                new PcasRevision { BoardCode = this.boardCode, PcasPartNumber = this.partNumber });

            this.PcasBoardsForAuditRepository.FindBy(Arg.Any<Expression<Func<PcasBoardForAudit, bool>>>()).Returns(
                new PcasBoardForAudit { BoardCode = this.boardCode, CutClinch = "Y", ForAudit = "Y" });

            this.result = this.Sut.GetWorksOrderDetails(this.partNumber);
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
        public void ShouldCallPcasBoardsForAuditRepository()
        {
            this.PcasBoardsForAuditRepository.Received().FindBy(Arg.Any<Expression<Func<PcasBoardForAudit, bool>>>());
        }

        [Test]
        public void ShouldReturnNull()
        {
            this.result.AuditDisclaimer.Should().Be("Board requires audit. Use cut and clinch tool");
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.PartDescription.Should().Be(this.partDescription);
            this.result.PartNumber.Should().Be(this.partNumber);
        }
    }
}