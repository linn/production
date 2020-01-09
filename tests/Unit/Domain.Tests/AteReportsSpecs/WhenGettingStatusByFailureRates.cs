namespace Linn.Production.Domain.Tests.AteReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NUnit.Framework;

    public class WhenGettingStatusByFailureRates : ContextBase
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
                AteReportGroupBy.FailureRates);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("ATE Failure Rate By Board");
        }

        [Test]
        public void ShouldSetCorrectValues()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(12);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("20-tests")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("20-fails")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("20-percentage")).Should().Be(0);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("21-tests")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("21-fails")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("21-percentage")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("22-tests")).Should().Be(5);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("22-fails")).Should().Be(3);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("22-percentage")).Should().Be(40);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("total-tests")).Should().Be(6);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("total-fails")).Should().Be(4);
            this.result.GetGridValue(this.result.RowIndex("part 1"), this.result.ColumnIndex("total-percentage")).Should().Be(33.3m);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("21-tests")).Should().Be(5);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("21-fails")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("21-percentage")).Should().Be(80m);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("total-tests")).Should().Be(5);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("total-fails")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("part 2"), this.result.ColumnIndex("total-percentage")).Should().Be(80m);
        }

        [Test]
        public void ShouldSetCorrectTotals()
        {
            this.result.GetTotalValue(this.result.ColumnIndex("20-tests")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("20-fails")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("20-percentage")).Should().Be(0);
            this.result.GetTotalValue(this.result.ColumnIndex("21-tests")).Should().Be(5);
            this.result.GetTotalValue(this.result.ColumnIndex("21-fails")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("21-percentage")).Should().Be(80);
            this.result.GetTotalValue(this.result.ColumnIndex("22-tests")).Should().Be(5);
            this.result.GetTotalValue(this.result.ColumnIndex("22-fails")).Should().Be(3);
            this.result.GetTotalValue(this.result.ColumnIndex("22-percentage")).Should().Be(40m);
            this.result.GetTotalValue(this.result.ColumnIndex("total-tests")).Should().Be(11);
            this.result.GetTotalValue(this.result.ColumnIndex("total-fails")).Should().Be(5);
            this.result.GetTotalValue(this.result.ColumnIndex("total-percentage")).Should().Be(54.5m);
        }
    }
}
