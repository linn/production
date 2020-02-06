namespace Linn.Production.Domain.Tests.ExtensionsSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Extensions;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NUnit.Framework;

    public class WhenAteGroupByBoard
    {
        private string result;

        [SetUp]
        public void SetUp()
        {
            this.result = AteReportGroupBy.Board.ParseOption();
        }

        [Test]
        public void ShouldGetCorrectOption()
        {
            this.result.Should().Be("board");
        }
    }
}