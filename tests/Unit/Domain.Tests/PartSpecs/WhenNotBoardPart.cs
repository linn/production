namespace Linn.Production.Domain.Tests.PartSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenNotBoardPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut = new Part { PartNumber = "OTHER PART" };
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.Sut.IsBoardPart().Should().BeFalse();
        }
    }
}