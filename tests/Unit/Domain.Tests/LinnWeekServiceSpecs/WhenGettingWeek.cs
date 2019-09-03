namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWeek : ContextBase
    {
        private LinnWeek result;

        [SetUp]
        public void SetUp()
        {
            this.LinnWeekRepository.GetWeek(1.August(2020))
                .Returns(new LinnWeek { LinnWeekNumber = 34 });

            this.result = this.Sut.GetWeek(1.August(2020));
        }

        [Test]
        public void ShouldGetWeekFromRepository()
        {
            this.LinnWeekRepository.Received().GetWeek(1.August(2020));
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.result.LinnWeekNumber.Should().Be(34);
        }
    }
}
