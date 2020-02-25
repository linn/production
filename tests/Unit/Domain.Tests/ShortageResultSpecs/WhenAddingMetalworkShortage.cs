namespace Linn.Production.Domain.Tests.ShortageResultSpecs
{
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using NUnit.Framework;

    public class WhenAddingMetalworkShortage : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var shortage = new WswShortage
            {
                PartNumber = "SK DISPLAY/3",
                ShortPartDescription = "MOLD 371/P",
                ShortageCategory = "CP",
                Required = 12,
                Stock = 0,
                AdjustedAvailable = 0,
                QtyReserved = 0
            };

            this.Sut.AddWswShortage(shortage);
        }

        [Test]
        public void ShouldBeMetalworkShortage()
        {
            this.Sut.BoardShortage.Should().Be(false);
            this.Sut.MetalworkShortage.Should().Be(true);
            this.Sut.ProcurementShortage.Should().Be(false);
        }
    }
}