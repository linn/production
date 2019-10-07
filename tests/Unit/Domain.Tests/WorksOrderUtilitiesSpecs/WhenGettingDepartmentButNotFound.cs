namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDepartmentButNotFound : ContextBase
    {
        private Action action;

        private string citCode;

        [SetUp]
        public void SetUp()
        {
            var partNumber = "MAJIK";

            this.citCode = "CIT";

            this.ProductionTriggerLevelsRepository.FindById(partNumber)
                .Returns(new ProductionTriggerLevel { CitCode = this.citCode });

            this.CitRepository.FindById(this.citCode).Returns(new Cit { DepartmentCode = "DEPT", Code = this.citCode });

            this.DepartmentRepository.FindById("DEPT").Returns((Department)null);

            this.action = () => this.Sut.GetDepartment(partNumber);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Department code not found for CIT {this.citCode}");
        }
    }
}