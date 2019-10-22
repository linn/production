namespace Linn.Production.Domain.Tests.PurchaseOrderSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenContainsPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.Details = new List<PurchaseOrderDetail>
                                   {
                                       new PurchaseOrderDetail { OrderNumber = 1, PartNumber = "PART" }
                                   };
        }

        [Test]
        public void ShouldReturnTrueIfContainsPart()
        {
            this.Sut.ContainsPart("PART").Should().BeTrue();
        }
    }
}