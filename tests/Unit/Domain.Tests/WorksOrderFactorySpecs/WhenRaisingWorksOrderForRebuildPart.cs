namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderForRebuildPart : ContextBase
    {
        private Action action;

        private string department;

        private string partNumber;

        private int raisedBy;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "TROIKA/R";
            this.department = "DEPT";
            this.raisedBy = 33067;

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A" });

            this.action = () => this.Sut.RaiseWorksOrder(this.partNumber, this.department, this.raisedBy);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Use Works Order Rebuild Utility for this part {this.partNumber}");
        }
    }
}