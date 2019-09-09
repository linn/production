namespace Linn.Production.Facade.Tests.ExtensionsSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Facade.Extensions;

    using NUnit.Framework;

    public class WhenAssemblyFailsMeasuresGroupByBoardPartNumber
    {
        private AssemblyFailGroupBy result;

        [SetUp]
        public void SetUp()
        {
            this.result = "board-part-number".ParseOption();
        }

        [Test]
        public void ShouldGetCorrectOption()
        {
            this.result.Should().Be(AssemblyFailGroupBy.BoardPartNumber);
        }
    }
}