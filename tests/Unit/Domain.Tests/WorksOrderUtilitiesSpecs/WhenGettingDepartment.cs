namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDepartment : ContextBase
    {
        private Department result;

        [SetUp]
        public void SetUp()
        {
            var partNumber = "MAJIK";

            this.ProductionTriggerLevelsRepository.FindById(partNumber)
                .Returns(new ProductionTriggerLevel { CitCode = "CIT" });

            this.CitRepository.FindById("CIT").Returns(new Cit { DepartmentCode = "DEPT" });

            this.DepartmentRepository.FindById("DEPT").Returns(new Department { DepartmentCode = "DEPT" });

            this.result = this.Sut.GetDepartment(partNumber);
        }

        [Test]
        public void ShouldReturnDepartment()
        {
            this.result.DepartmentCode.Should().Be("DEPT");
        }
    }
}
