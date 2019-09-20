namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWoDetailsWhenNoMatchingPcasRevision : ContextBase
    {
        private string partNumber;

        private string partDescription;

        private WorksOrderPartDetails result;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "PCAS 123";
            this.partDescription = "DESCRIPTION";

            this.PartsRepository.FindById(this.partNumber)
                .Returns(new Part { PartNumber = this.partNumber, Description = this.partDescription });

            this.PcasRevisionsRepository.FindBy(Arg.Any<Expression<Func<PcasRevision, bool>>>()).Returns((PcasRevision)null);

            this.ProductionTriggerLevelsRepository.FindById(this.partNumber)
                .Returns(new ProductionTriggerLevel { CitCode = "CIT" });

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
        public void ShouldReturnNull()
        {
            this.result.AuditDisclaimer.Should().BeNullOrEmpty();
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.PartDescription.Should().Be(this.partDescription);
            this.result.PartNumber.Should().Be(this.partNumber);
        }
    }
}
