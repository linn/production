namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenPartNotConsistentWithPurchaseOrder : ContextBase
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

            this.PartRepository.FindById("PART").Returns(new Part { PartNumber = "PART" });
            this.PurchaseOrderRepository.FindById(1).Returns(
                new PurchaseOrder
                    {
                        OrderNumber = 1,
                        Details = new List<PurchaseOrderDetail>
                                      {
                                          new PurchaseOrderDetail
                                              {
                                                  PartNumber = "OTHER"
                                              }
                                      }
                    });
        }

        [Test]
        public void ShouldThrowInvalidWorksOrderException()
        {
            Assert.Throws<InvalidPurchaseOrderException>(() => this.Sut.Check(this.Candidate));
        }
    }
}