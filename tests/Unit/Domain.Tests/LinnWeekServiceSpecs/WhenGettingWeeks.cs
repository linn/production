namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWeeks : ContextBase
    {
        private IEnumerable<LinnWeek> results;

        [SetUp]
        public void SetUp()
        {
            var weeks = new List<LinnWeek>
                            {
                                new LinnWeek { LinnWeekNumber = 34 },
                                new LinnWeek { LinnWeekNumber = 35 },
                                new LinnWeek { LinnWeekNumber = 36 }
                            };

            this.LinnWeekRepository.GetWeeks(1.August(2020), 1.December(2020))
                .Returns(weeks);

            this.results = this.Sut.GetWeeks(1.August(2020), 1.December(2020));
        }

        [Test]
        public void ShouldGetWeeksFromRepository()
        {
            this.LinnWeekRepository.Received().GetWeeks(1.August(2020), 1.December(2020));
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Should().HaveCount(3);
            this.results.Should().Contain(w => w.LinnWeekNumber == 34);
            this.results.Should().Contain(w => w.LinnWeekNumber == 35);
            this.results.Should().Contain(w => w.LinnWeekNumber == 36);
        }
    }
}
