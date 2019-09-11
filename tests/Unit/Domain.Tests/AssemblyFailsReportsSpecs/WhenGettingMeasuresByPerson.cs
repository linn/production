namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NUnit.Framework;

    public class WhenGettingMeasuresByPerson : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetAssemblyFailsMeasuresReport(
                1.June(2020),
                30.June(2020),
                AssemblyFailGroupBy.Person);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Assembly Fails Measures Grouped By Person");
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(4);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("1"), this.result.ColumnIndex("20")).Should().Be(3);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("1"), this.result.ColumnIndex("Total")).Should().Be(3);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("2"), this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("2"), this.result.ColumnIndex("Total")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("20")).Should().Be(3);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("Total")).Should().Be(5);
        }
    }
}
