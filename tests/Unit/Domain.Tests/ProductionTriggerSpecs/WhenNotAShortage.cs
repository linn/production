namespace Linn.Production.Domain.Tests.ProductionTriggerSpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenNotAShortage : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.RemainingBuild = null;
            this.Sut.ReqtForInternalAndTriggerLevelBT = 6;
            this.Sut.CanBuild = 10;
            this.Sut.QtyBeingBuilt = 4;
        }

        [Test]
        public void ShouldBeAShortage()
        {
            this.Sut.IsShortage().Should().BeFalse();
        }
    }
}