namespace Linn.Production.Domain.Tests.ProductionTriggerAssemblySpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenHasReqtOnNettSales : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.NettSalesOrders = 1;
        }

        [Test]
        public void ShouldHaveReqt()
        {
            this.Sut.HasReqt().Should().BeTrue();
        }
    }
}