namespace Linn.Production.Domain.Tests.ShortageResultSpecs
{
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using NUnit.Framework;

    public class WhenAddingProcurementShortage : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var shortage = new WswShortage
            {
                PartNumber = "S3 302/C",
                ShortPartNumber = "MCP 2466/2",
                ShortPartDescription = "SERIES 3 CABINET",
                ShortageCategory = "PROC",
                Required = 8,
                Stock = 11,
                AdjustedAvailable = 10,
                QtyReserved = 0
            };

            this.Sut.AddWswShortage(shortage);
        }

        [Test]
        public void ShouldBeBoardShortage()
        {
            this.Sut.BoardShortage.Should().Be(false);
            this.Sut.MetalworkShortage.Should().Be(false);
            this.Sut.ProcurementShortage.Should().Be(true);
        }
    }
}