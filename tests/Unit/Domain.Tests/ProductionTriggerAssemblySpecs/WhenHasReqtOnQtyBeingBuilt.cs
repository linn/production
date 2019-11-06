namespace Linn.Production.Domain.Tests.ProductionTriggerAssemblySpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenHasReqtOnQtyBeingBuilt : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.QtyBeingBuilt = 1;
        }

        [Test]
        public void ShouldHaveReqt()
        {
            this.Sut.HasReqt().Should().BeTrue();
        }
    }
}