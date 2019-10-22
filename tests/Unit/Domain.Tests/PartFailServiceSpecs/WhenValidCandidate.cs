namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidCandidate : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Candidate = new PartFail
                                 {
                                     Id = 1,
                                     Part = new Part { PartNumber = "PART" },
                                     PurchaseOrderNumber = 1,
                                     WorksOrder = new WorksOrder { OrderNumber = 1 }
                                 };

            this.PartRepository.FindById("PART").Returns(new Part { PartNumber = "PART" });
            this.WorksOrderRepository.FindById(1).Returns(new WorksOrder { OrderNumber = 1 });
            this.PurchaseOrderRepository.FindById(1).Returns(
                new PurchaseOrder
                    {
                        OrderNumber = 1,
                        Details = new List<PurchaseOrderDetail>
                                      {
                                          new PurchaseOrderDetail
                                              {
                                                  PartNumber = "PART"
                                              }
                                      }
                    });
        }

        [Test]
        public void ShouldReturnPartFail()
        {
            this.Sut.Check(Candidate).Should().BeOfType<PartFail>();
        }
    }
}