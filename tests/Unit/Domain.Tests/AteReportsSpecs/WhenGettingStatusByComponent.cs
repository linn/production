namespace Linn.Production.Domain.Tests.AteReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NUnit.Framework;

    public class WhenGettingStatusByComponent : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetStatusReport(
                1.June(2020),
                30.June(2020),
                "SMT",
                "ATE",
                AteReportGroupBy.Component);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("ATE Test Fails By Component");
        }

        [Test]
        public void ShouldSetCorrectValues()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(4);
            this.result.GetGridValue(this.result.RowIndex("comp 1"), this.result.ColumnIndex("20")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("comp 1"), this.result.ColumnIndex("21")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("comp 1"), this.result.ColumnIndex("22")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("comp 1"), this.result.ColumnIndex("Total")).Should().Be(2);
            this.result.GetGridValue(this.result.RowIndex("comp 2"), this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("comp 2"), this.result.ColumnIndex("21")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("comp 2"), this.result.ColumnIndex("22")).Should().Be(2);
            this.result.GetGridValue(this.result.RowIndex("comp 2"), this.result.ColumnIndex("Total")).Should().Be(3);
            this.result.GetTotalValue(this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("21")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("22")).Should().Be(3);
            this.result.GetTotalValue(this.result.ColumnIndex("Total")).Should().Be(5);
        }
    }
}
