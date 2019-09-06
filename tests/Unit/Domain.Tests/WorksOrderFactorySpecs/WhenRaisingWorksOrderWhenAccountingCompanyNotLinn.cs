namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderWhenAccountingCompanyNotLinn : ContextBase
    {
        private WorksOrder result;

        private string department;

        private string partNumber;

        private int raisedBy;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.department = "DEPT";
            this.raisedBy = 33067;

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A", AccountingCompany = "OTHER" });

            this.WorksOrderService.CanRaiseWorksOrder(this.partNumber).Returns("SUCCESS");

            this.WorksOrderService.GetDepartment(this.partNumber, this.department).Returns("SUCCESS");

            this.result = this.Sut.RaiseWorksOrder(this.partNumber, this.department, this.raisedBy);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.result.PartNumber.Should().Be(this.partNumber);
            this.result.RaisedByDepartment.Should().Be("PIK ASSY");
        }
    }
}