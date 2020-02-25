namespace Linn.Production.Domain.Tests.ShortageSummarySpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps.Models;
    using NUnit.Framework;

    public class WhenGettingPercentages
    {
        private ShortageSummary sut;

        [SetUp]
        public void SetUp()
        {
            this.sut = new ShortageSummary
            {
                OnesTwos = 10,
                Shortages = new List<ShortageResult>
                {
                    new ShortageResult
                    {
                        BoardShortage = true,
                        MetalworkShortage = false,
                        ProcurementShortage = false
                    },
                    new ShortageResult
                    {
                        BoardShortage = true,
                        MetalworkShortage = false,
                        ProcurementShortage = false
                    },
                    new ShortageResult
                    {
                        BoardShortage = false,
                        MetalworkShortage = false,
                        ProcurementShortage = true
                    },
                    new ShortageResult
                    {
                        BoardShortage = false,
                        MetalworkShortage = false,
                        ProcurementShortage = true
                    },
                    new ShortageResult
                    {
                        BoardShortage = false,
                        MetalworkShortage = true,
                        ProcurementShortage = false
                    },
                    new ShortageResult
                    {
                        BoardShortage = false,
                        MetalworkShortage = false,
                        ProcurementShortage = true
                    }
                }
            };
        }

        [Test]
        public void NumShortagesPercShouldBe60()
        {
            this.sut.ShortagePerc().Should().Be(60);
        }

        [Test]
        public void BATPercShouldBe20()
        {
            this.sut.BATPerc().Should().Be(20);
        }

        [Test]
        public void MetalworkPercShouldBe10()
        {
            this.sut.MetalworkPerc().Should().Be(10);
        }

        [Test]
        public void ProcurementPercShouldBe30()
        {
            this.sut.ProcurementPerc().Should().Be(30);
        }
    }
}
