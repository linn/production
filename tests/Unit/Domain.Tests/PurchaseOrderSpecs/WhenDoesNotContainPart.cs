namespace Linn.Production.Domain.Tests.PurchaseOrderSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenDoesNotContainPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.Details = new List<PurchaseOrderDetail>
                                   {
                                       new PurchaseOrderDetail { OrderNumber = 1, PartNumber = "NOPE" }
                                   };
        }

        [Test]
        public void ShouldReturnTrueIfContainsPart()
        {
            this.Sut.ContainsPart("PART").Should().BeFalse();
        }
    }
}