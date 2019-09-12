namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NUnit.Framework;

    public class WhenCancellingWorksOrder : ContextBase
    {
        private WorksOrder result;

        private int? cancelledBy;

        private string reasonCancelled;

        [SetUp]
        public void SetUp()
        {
            var worksOrder = new WorksOrder { PartNumber = "MAJIK" };

            this.cancelledBy = 33067;
            this.reasonCancelled = "REASON";

            this.result = this.Sut.CancelWorksOrder(
                worksOrder,
                this.cancelledBy,
                this.reasonCancelled);
        }

        [Test]
        public void ShouldUpdateWorksOrder()
        {
            this.result.CancelledBy.Should().Be(this.cancelledBy);
            this.result.ReasonCancelled.Should().Be(this.reasonCancelled);
            this.result.DateCancelled.Should().NotBeNull();
            this.result.PartNumber.Should().Be("MAJIK");
        }
    }
}
