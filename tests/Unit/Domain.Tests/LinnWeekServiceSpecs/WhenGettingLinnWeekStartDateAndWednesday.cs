namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Extensions;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingLinnWeekStartDateAndWednesday : ContextBase
    {
        private DateTime result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.LinnWeekStartDate(new DateTime(2020, 1, 15));
        }

        [Test]
        public void ShouldBeTheSaturday()
        {
            this.result.Date.Day.Should().Be(11);
            this.result.Date.Month.Should().Be(1);
            this.result.Date.Year.Should().Be(2020);
        }
    }
}
