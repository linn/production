namespace Linn.Production.Domain.Tests.PtlStatPrioritySummarySpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using NUnit.Framework;

    public class WhenConstructingPtlStatPrioritySummaryFor3DayTarget
    {
        protected PtlStatPrioritySummary Sut { get; set; }

        [SetUp]
        public void SetUp()
        {
            var stats = new List<PtlStat>()
            {
                new PtlStat { PtlPriority = 1, WorkingDays = 0m, BuildGroup = "CP" },
                new PtlStat { PtlPriority = 1, WorkingDays = 1m, BuildGroup = "CP" },
                new PtlStat { PtlPriority = 1, WorkingDays = 2m, BuildGroup = "CP" },
                new PtlStat { PtlPriority = 1, WorkingDays = 4m, BuildGroup = "CP" },
                new PtlStat { PtlPriority = 1, WorkingDays = 8m, BuildGroup = "CP" }
            };

            this.Sut = new PtlStatPrioritySummary(1);
            foreach (var stat in stats)
            {
                this.Sut.AddStatToSummary(stat);
            }
        }

        [Test]
        public void ShouldBeForPriority1()
        {
            this.Sut.Priority.Should().Be(1);
        }

        [Test]
        public void ShouldHaveThreeTriggers()
        {
            this.Sut.Triggers.Should().Be(5);
        }

        [Test]
        public void ShouldHaveCorrectDayCounts()
        {
            this.Sut.OneDay.Should().Be(2);
            this.Sut.TwoDay.Should().Be(1);
            this.Sut.ThreeDay.Should().Be(0);
            this.Sut.FourDay.Should().Be(1);
            this.Sut.FiveDay.Should().Be(0);
            this.Sut.Gt5Day.Should().Be(1);
        }

        [Test]
        public void ShouldHaveCorrectAvgTurnaround()
        {
            this.Sut.AvgTurnaround().Should().Be(3);
        }

        [Test]
        public void ShouldHaveCorrectpercByTargetDays()
        {
            this.Sut.PercByTargetDays().Should().Be(60);
        }
    }
}