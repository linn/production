namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWorksOrderDetails : ContextBase
    {
        private string partNumber;

        private string partDescription;

        private string boardCode;

        private string workStationCode;

        private int quantity;

        private WorksOrderPartDetails result;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";
            this.partDescription = "DESCRIPTION";
            this.boardCode = "123AB";
            this.workStationCode = "STATION";
            this.quantity = 10;

            this.PartsRepository.FindById(this.partNumber)
                .Returns(new Part { PartNumber = this.partNumber, Description = this.partDescription });

            this.ProductionTriggerLevelsRepository.FindById(this.partNumber).Returns(
                new ProductionTriggerLevel()
                    {
                        PartNumber = this.partNumber,
                        WsName = this.workStationCode,
                        CitCode = "CIT",
                        KanbanSize = this.quantity
                    });

            this.PcasRevisionsRepository.FindBy(Arg.Any<Expression<Func<PcasRevision, bool>>>()).Returns(
                new PcasRevision { BoardCode = this.boardCode, PcasPartNumber = this.partNumber });

            this.PcasBoardsForAuditRepository.FindBy(Arg.Any<Expression<Func<PcasBoardForAudit, bool>>>()).Returns(
                new PcasBoardForAudit { BoardCode = this.boardCode, CutClinch = "N", ForAudit = "Y" });

            this.CitRepository.FindById("CIT").Returns(new Cit { DepartmentCode = "DEPT" });

            this.DepartmentRepository.FindById("DEPT").Returns(new Department { DepartmentCode = "DEPT" });

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
        public void ShouldProductionTriggerLevelRepository()
        {
            this.ProductionTriggerLevelsRepository.Received().FindById(this.partNumber);
        }

        [Test]
        public void ShouldReturnWorksOrderDetails()
        {
            this.result.AuditDisclaimer.Should().Be("Board requires audit");
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.PartDescription.Should().Be(this.partDescription);
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.WorkStationCode.Should().Be(this.workStationCode);
            this.result.QuantityToBuild.Should().Be(this.quantity);
        }
    }
}