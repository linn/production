namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenInvalidPartNumber : ContextBase
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

            this.PartRepository.FindById("PART").ReturnsNull();
        }

        [Test]
        public void ShouldThrowInvalidWorksOrderException()
        {
            Assert.Throws<InvalidPartNumberException>(() => this.Sut.Create(this.Candidate));
        }
    }
}