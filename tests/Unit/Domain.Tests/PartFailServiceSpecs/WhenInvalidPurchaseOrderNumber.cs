namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenInvalidPurchaseOrderNumber : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Candidate = new PartFail
                                 {
                                     Id = 1,
                                     Part = new Part { PartNumber = "PART" },
                                     PurchaseOrderNumber = 1
                                 };

            this.PartRepository.FindById("PART").Returns(new Part());
            this.PurchaseOrderRepository.FindById(1).ReturnsNull();
        }

        [Test]
        public void ShouldThrowInvalidWorksOrderException()
        {
            Assert.Throws<InvalidPurchaseOrderException>(() => this.Sut.Create(this.Candidate));
        }
    }
}