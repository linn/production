namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenGettingWeekFromList : ContextBase
    {
        private LinnWeek result;

        [SetUp]
        public void SetUp()
        {
            var weeks = new List<LinnWeek>
                            {
                                new LinnWeek { LinnWeekNumber = 34 },
                                new LinnWeek { LinnWeekNumber = 35, StartDate = 31.July(2020), EndDate = 6.August(2020) },
                                new LinnWeek { LinnWeekNumber = 36 }
                            };

            this.result = this.Sut.GetWeek(1.August(2020), weeks);
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.result.LinnWeekNumber.Should().Be(35);
        }
    }
}
