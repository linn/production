namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenInvalidWorksOrderNumber : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Candidate = new PartFail
                                 {
                                     Id = 1,
                                     Part = new Part { PartNumber = "PART" },
                                     WorksOrder = new WorksOrder { OrderNumber = 1 }
                                 };

            this.WorksOrderRepository.FindById(1).ReturnsNull();
        }

        [Test]
        public void ShouldThrowInvalidWorksOrderException()
        {
            Assert.Throws<InvalidWorksOrderException>(() => this.Sut.Create(this.Candidate));
        }
    }
}