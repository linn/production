namespace Linn.Production.Domain.Tests.PartSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenPcsmPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut = new Part { PartNumber = "PCSM PART" };
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.Sut.IsBoardPart().Should().BeTrue();
        }
    }
}