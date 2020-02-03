namespace Linn.Production.Domain.Tests.ProductionTriggerSpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenShortageFromRemainingBuild : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.RemainingBuild = 10;
            this.Sut.CanBuild = 8;
            this.Sut.QtyBeingBuilt = null;
        }

        [Test]
        public void ShouldBeAShortage()
        {
            this.Sut.IsShortage().Should().BeTrue();
        }
    }
}