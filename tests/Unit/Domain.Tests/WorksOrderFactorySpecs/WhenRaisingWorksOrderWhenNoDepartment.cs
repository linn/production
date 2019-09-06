namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderWhenNoDepartment : ContextBase
    {
        private Action action;

        private string department;

        private string partNumber;

        private int raisedBy;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.department = "DEPT";
            this.raisedBy = 33067;

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A", AccountingCompany = "LINN" });

            this.WorksOrderService.CanRaiseWorksOrder(this.partNumber).Returns("SUCCESS");

            this.WorksOrderService.GetDepartment(this.partNumber, this.department).Returns("Error");

            this.action = () => this.Sut.RaiseWorksOrder(this.partNumber, this.department, this.raisedBy);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Error");
        }
    }
}