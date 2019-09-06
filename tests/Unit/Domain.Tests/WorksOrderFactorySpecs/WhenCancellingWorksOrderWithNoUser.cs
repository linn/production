namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NUnit.Framework;

    public class WhenCancellingWorksOrderWithNoUser : ContextBase
    {
        private Action action;

        private int? cancelledBy;

        private string reasonCancelled;

        [SetUp]
        public void SetUp()
        {
            var worksOrder = new WorksOrder { PartNumber = "MAJIK" };

            this.cancelledBy = null;
            this.reasonCancelled = "REASON";

            this.action = () => this.Sut.CancelWorksOrder(worksOrder, this.cancelledBy, this.reasonCancelled);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage("You must provide a user number and reason when cancelling a works order");
        }
    }
}