namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;

    using NUnit.Framework;

    public class WhenGettingDetails : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetAssemblyFailsDetailsReport(
                1.June(2020),
                30.June(2020),
                "Board 1",
                "Circuit Part 1",
                "F1",
                "C");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Assembly fails between 01-Jun-2020 and 30-Jun-2020. Board part number is Board 1 Circuit part number is Circuit Part 1 Fault code is F1 Cit code is C ");
            this.result.Rows.Should().HaveCount(2);
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Week")).Should().Be("25/20");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("PartNumber")).Should().Be("W O Part");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("BoardPartNumber")).Should().Be("Board 1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Fails")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("CircuitPartNumber")).Should().Be("Circuit Part 1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("FaultCode")).Should().Be("F1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("ReportedFault")).Should().Be("report");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Analysis")).Should().Be("analysis");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Cit")).Should().Be("Cit 1");
        }
    }
}
