namespace Linn.Production.Domain.Tests.ProductionTriggerAssemblySpecs
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenHasNoReqt : ContextBase
    {
        [Test]
        public void ShouldHaveReqt()
        {
            this.Sut.HasReqt().Should().BeFalse();
        }
    }
}