namespace Linn.Production.Facade.Tests.ExtensionsSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Facade.Extensions;

    using NUnit.Framework;

    public class WhenAteGroupByComponent
    {
        private AteReportGroupBy result;

        [SetUp]
        public void SetUp()
        {
            this.result = "component".ParseAteReportOption();
        }

        [Test]
        public void ShouldGetCorrectOption()
        {
            this.result.Should().Be(AteReportGroupBy.Component);
        }
    }
}