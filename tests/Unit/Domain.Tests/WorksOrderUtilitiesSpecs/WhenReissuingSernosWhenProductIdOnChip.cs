namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenReissuingSernosWhenProductIdOnChip : ContextBase
    {
        private string partNumber;

        private int orderNumber;

        private string docType;

        private int createdBy;

        private int quantity;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.orderNumber = 123;
            this.docType = "A";
            this.createdBy = 33067;
            this.quantity = 3;

            this.SalesArticleService.ProductIdOnChip(this.partNumber).Returns(true);

            this.Sut.IssueSerialNumber(this.partNumber, this.orderNumber, this.docType, this.createdBy, this.quantity);
        }

        [Test]
        public void ShouldNotCheckIfSerialNumbersRequired()
        {
            this.SernosPack.DidNotReceive().SerialNumbersRequired(this.partNumber);
        }

        [Test]
        public void ShouldNotIssueSerialNumber()
        {
            this.SernosPack.DidNotReceive().IssueSernos(
                this.orderNumber,
                this.docType,
                0,
                this.partNumber,
                this.createdBy,
                this.quantity,
                null);
        }
    }
}