namespace Linn.Production.Domain.Tests.SmtReportsSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NUnit.Framework;

    public class WhenGettingOutstandingPartsForSelectedParts : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.OutstandingWorksOrderParts("All", new[] { "P2" });
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Components required for outstanding SMT works orders");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(3);
            var row1 = this.result.Rows.First(a => a.SortOrder == -1);
            this.result.GetGridValue(row1.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(14);
            this.result.GetGridTextValue(row1.RowIndex, this.result.ColumnIndex("Component")).Should().Be("P2");
            var row2 = this.result.Rows.First(a => a.SortOrder == 0);
            this.result.GetGridValue(row2.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(4);
            this.result.GetGridTextValue(row2.RowIndex, this.result.ColumnIndex("Board")).Should().Be("B2");
            var row3 = this.result.Rows.First(a => a.SortOrder == 10);
            this.result.GetGridValue(row3.RowIndex, this.result.ColumnIndex("Qty Required")).Should().Be(10);
            this.result.GetGridTextValue(row3.RowIndex, this.result.ColumnIndex("Board")).Should().Be("B2");
        }
    }
}
