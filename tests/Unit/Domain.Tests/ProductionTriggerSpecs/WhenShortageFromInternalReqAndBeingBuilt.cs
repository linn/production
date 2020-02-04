namespace Linn.Production.Domain.Tests.ProductionTriggerSpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenShortageFromInternalReqAndBeingBuilt : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.RemainingBuild = null;
            this.Sut.ReqtForInternalAndTriggerLevelBT = 6;
            this.Sut.CanBuild = 8;
            this.Sut.QtyBeingBuilt = 4;
        }

        [Test]
        public void ShouldBeAShortage()
        {
            this.Sut.IsShortage().Should().BeTrue();
        }
    }
}