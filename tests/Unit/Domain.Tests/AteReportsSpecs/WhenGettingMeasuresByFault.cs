namespace Linn.Production.Domain.Tests.AteReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NUnit.Framework;

    public class WhenGettingStatusByFault : ContextBase
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
                AteReportGroupBy.FaultCode);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("ATE Test Fails By Fault Code");
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(4);
            this.result.GetGridValue(this.result.RowIndex("fault 1"), this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("fault 1"), this.result.ColumnIndex("21")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("fault 1"), this.result.ColumnIndex("22")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("fault 1"), this.result.ColumnIndex("Total")).Should().Be(2);
            this.result.GetGridValue(this.result.RowIndex("fault 2"), this.result.ColumnIndex("20")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("fault 2"), this.result.ColumnIndex("21")).Should().BeNull();
            this.result.GetGridValue(this.result.RowIndex("fault 2"), this.result.ColumnIndex("22")).Should().Be(1);
            this.result.GetGridValue(this.result.RowIndex("fault 2"), this.result.ColumnIndex("Total")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("21")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("22")).Should().Be(1);
            this.result.GetTotalValue(this.result.ColumnIndex("Total")).Should().Be(3);
        }
    }
}
