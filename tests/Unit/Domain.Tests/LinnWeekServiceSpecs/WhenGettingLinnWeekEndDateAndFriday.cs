namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingLinnWeekEndDateAndFriday : ContextBase
    {
        private DateTime result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.LinnWeekEndDate(new DateTime(2020, 1, 17));
        }

        [Test]
        public void ShouldBeTheSaturday()
        {
            this.result.Date.Day.Should().Be(17);
            this.result.Date.Month.Should().Be(1);
            this.result.Date.Year.Should().Be(2020);
        }
    }
}