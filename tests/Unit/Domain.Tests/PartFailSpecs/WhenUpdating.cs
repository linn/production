namespace Linn.Production.Domain.Tests.PartFailSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PartFail updatedFail;

        [SetUp]
        public void SetUp()
        {
            this.updatedFail = new PartFail
                                 {
                                     Id = 13434,
                                     Part = new Part { PartNumber = "PART" },
                                     SerialNumber = 1,
                                     PurchaseOrderNumber = 1,
                                     WorksOrder = new WorksOrder { OrderNumber = 1 }
                                 };

            this.Sut.UpdateFrom(this.updatedFail);
        }

        [Test]
        public void ShouldUpdatePartFail()
        {
            this.Sut.Id.Should().Be(246);
            this.Sut.SerialNumber.Should().Be(this.updatedFail.SerialNumber);
        }
    }
}