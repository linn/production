namespace Linn.Production.Domain.Tests.ProductionTriggerAssemblySpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenHasReqtOnInternalReqt : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.ReqtForInternalAndTriggerLevelBT = 1;
        }

        [Test]
        public void ShouldHaveReqt()
        {
            this.Sut.HasReqt().Should().BeTrue();
        }
    }
}