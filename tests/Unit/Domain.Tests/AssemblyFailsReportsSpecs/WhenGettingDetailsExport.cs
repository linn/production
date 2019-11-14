namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;

    using NUnit.Framework;

    public class WhenGettingDetailsExport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetAssemblyFailsDetailsReportExport(1.June(2020), 30.June(2020));
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Assembly fails between 01-Jun-2020 and 30-Jun-2020. ");
            this.result.Rows.Should().HaveCount(3);
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Week")).Should().Be("25/20");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Date Found")).Should().Be("01-Jun-2020");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("PartNumber")).Should().Be("W O Part");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("BoardPartNumber")).Should().Be("Board 1/1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Fails")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("CircuitPartNumber")).Should().Be("Circuit Part 1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("FaultCode")).Should().Be("F1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("ReportedFault")).Should().Be("report");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Analysis")).Should().Be("analysis");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Cit")).Should().Be("Cit 1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Entered By")).Should().Be("Jane Assembler");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("Completed By")).Should().Be("John Engineer");
        }
    }
}
