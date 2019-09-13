﻿namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderForPhantomPart : ContextBase
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

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "P" });

            this.action = () => this.Sut.RaiseWorksOrder(new WorksOrder
                                                             {
                                                                 PartNumber = this.partNumber,
                                                                 RaisedByDepartment = this.department,
                                                                 RaisedBy = this.raisedBy
                                                             });
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Cannot raise a works order for phantom part {this.partNumber}");
        }
    }
}