namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenIssuingSernosAndStoredProcedureErrors : ContextBase
    {
        private string partNumber;

        private int orderNumber;

        private string docType;

        private int createdBy;

        private int quantity;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.orderNumber = 123;
            this.docType = "A";
            this.createdBy = 33067;
            this.quantity = 3;

            this.SalesArticleService.ProductIdOnChip(this.partNumber).Returns(false);

            this.SernosPack.When(x => x.SerialNumbersRequired(this.partNumber)).Do(z => throw new Exception("Bad things happened"));

            this.action = () => this.Sut.IssueSerialNumber(this.partNumber, this.orderNumber, this.docType, this.createdBy, this.quantity);
        }

        [Test]
        public void ShouldThrowIssueSerialNumberException()
        {
             this.action.Should().Throw<IssueSerialNumberException>()
                 .WithMessage(
                     $"Error Issuing serial numbers. Does {partNumber} have a PRODUCT ANALYSIS CODE and SERNOS SEQUENCE set?  Message for IT: Bad things happened");
        }
    }
}
