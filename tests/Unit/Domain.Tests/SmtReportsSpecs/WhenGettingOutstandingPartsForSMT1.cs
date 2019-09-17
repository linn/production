namespace Linn.Production.Domain.Tests.SmtReportsSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NUnit.Framework;

    public class WhenGettingOutstandingPartsForSMT1 : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.OutstandingWorksOrderParts("SMT1", new string[0]);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Components required for outstanding SMT works orders");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(4);
            var row1 = this.result.Rows.First(a => a.SortOrder == -1);
            this.result.GetGridValue(row1.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(8);
            this.result.GetGridTextValue(row1.RowIndex, this.result.ColumnIndex("Component")).Should().Be("P1");
            var row2 = this.result.Rows.First(a => a.SortOrder == 0);
            this.result.GetGridValue(row2.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(8);
            this.result.GetGridTextValue(row2.RowIndex, this.result.ColumnIndex("Board")).Should().Be("B1");
            var row3 = this.result.Rows.First(a => a.SortOrder == 9);
            this.result.GetGridValue(row3.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(4);
            this.result.GetGridTextValue(row3.RowIndex, this.result.ColumnIndex("Component")).Should().Be("P2");
            var row4 = this.result.Rows.First(a => a.SortOrder == 10);
            this.result.GetGridValue(row4.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(4);
            this.result.GetGridTextValue(row4.RowIndex, this.result.ColumnIndex("Board")).Should().Be("B2");
        }
    }
}
