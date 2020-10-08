namespace Linn.Production.Domain.Tests.ShortageResultSpecs
{
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using NUnit.Framework;

    public class WhenAddingBATShortage : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var shortage = new WswShortage
            {
                PartNumber = "S3 301/C",
                ShortPartNumber = "301 TOP",
                ShortPartDescription = "301 TOP",
                ShortageCategory = "EP",
                Required = 46,
                Stock = 9,
                AdjustedAvailable = 4,
                QtyReserved = 9
            };

            this.Sut.AddWswShortage(shortage);
        }

        [Test]
        public void ShouldBeBoardShortage()
        {
            this.Sut.BoardShortage.Should().Be(true);
            this.Sut.MetalworkShortage.Should().Be(false);
            this.Sut.ProcurementShortage.Should().Be(false);
        }
    }
}