namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenGettingDepartmentAndNoTriggerLevel : ContextBase
    {
        private string partNumber;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";

            this.ProductionTriggerLevelsRepository.FindById(this.partNumber)
                .ReturnsNull();

            this.CitRepository.FindById("CIT").Returns(new Cit { Code = "CIT" });

            this.DepartmentRepository.FindById("DEPT").Returns(new Department { DepartmentCode = "DEPT" });
        }

        [Test]
        public void ShouldReturnDepartment()
        {
            Assert.Throws<InvalidWorksOrderException>(() => this.Sut.GetDepartment(this.partNumber));
        }
    }
}