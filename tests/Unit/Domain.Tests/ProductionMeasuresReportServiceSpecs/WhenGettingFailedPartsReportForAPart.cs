namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFailedPartsReportForAPart : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.results = this.Sut.FailedPartsReport(null, "p1", null);
        }

        [Test]
        public void ShouldGetData()
        {
            this.FailedPartsRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnCorrectResult()
        {
            this.results.Should().HaveCount(1);
            var report = this.results.First();
            report.Rows.Should().HaveCount(2);
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Part Number")).Should().Be("p1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Part Number")).Should().Be("p1");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Description")).Should().Be("p1 desc");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Description")).Should().Be("p1 desc");
            report.GetGridValue(report.RowIndex("0"), report.ColumnIndex("Qty")).Should().Be(2);
            report.GetGridValue(report.RowIndex("1"), report.ColumnIndex("Qty")).Should().Be(2);
            report.GetGridValue(report.RowIndex("0"), report.ColumnIndex("Total Value")).Should().Be(34.34m);
            report.GetGridValue(report.RowIndex("1"), report.ColumnIndex("Total Value")).Should().Be(808.08m);
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("User Name")).Should().Be("Person 1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("User Name")).Should().Be("Person 1");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Storage Place")).Should().Be("Store 1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Storage Place")).Should().Be("Store 34");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Supplier Id")).Should().Be("1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Supplier Id")).Should().Be("1");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Supplier Name")).Should().Be("s1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Supplier Name")).Should().Be("s1");
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("CIT")).Should().Be("C Name");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("CIT")).Should().Be("D Name");
        }
    }
}
