namespace Linn.Production.Domain.Tests.ProductionTriggerSpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenShortageFromInternalReq : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.RemainingBuild = null;
            this.Sut.ReqtForInternalAndTriggerLevelBT = 10;
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