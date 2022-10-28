namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndStoryTooLong : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var story = string.Empty;

            for (int i = 0; i <= 200; i++)
            {
                story += "a";
            }

            this.Candidate = new PartFail
                                 {
                                     Id = 1,
                                     Part = new Part { PartNumber = "PART" },
                                     PurchaseOrderNumber = 1,
                                     Story = story
                                 };

            this.PartRepository.FindById("PART").Returns(new Part { PartNumber = "PART" });
            this.PurchaseOrderRepository.FindById(1).Returns(new PurchaseOrder
                                                                 {
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
        public void ShouldThrowException()
        {
            Assert.Throws<PartFailException>(() => this.Sut.Create(this.Candidate));
        }
    }
}
