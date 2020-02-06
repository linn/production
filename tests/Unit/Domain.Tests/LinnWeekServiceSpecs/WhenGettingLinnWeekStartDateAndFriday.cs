namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingLinnWeekStartDateAndFriday : ContextBase
    {
        private DateTime result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.LinnWeekStartDate(new DateTime(2020, 1, 17));
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