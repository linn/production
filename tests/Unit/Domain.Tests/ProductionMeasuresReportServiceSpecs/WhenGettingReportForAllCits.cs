namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReportForAllCits : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.results = this.Sut.FailedPartsReport(null, null, null, false, null);
        }

        [Test]
        public void ShouldGetData()
        {
            this.FailedPartsRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnTwoReports()
        {
            this.results.Should().HaveCount(2);
        }

        [Test]
        public void ShouldReturnResultsForFirstCit()
        {
            var report1 = this.results.First(a => a.ReportTitle.DisplayValue == "C Name");
            report1.Rows.Should().HaveCount(3);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Part Number")).Should().Be("p1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Part Number")).Should().Be("p2");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Description")).Should().Be("p1 desc");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Description")).Should().Be("p2 desc");
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Qty")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Qty")).Should().Be(34);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Total Value")).Should().Be(34.34m);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Total Value")).Should().Be(10m);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Date Booked")).Should().Be("01-Aug-2021");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("User Name")).Should().Be("Person 1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("User Name")).Should().Be("Person 2");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Storage Place")).Should().Be("Store 1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Storage Place")).Should().Be("Store 2");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Supplier Id")).Should().Be("1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Supplier Id")).Should().Be("2");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Supplier Name")).Should().Be("s1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Supplier Name")).Should().Be("s2");
        }

        [Test]
        public void ShouldReturnResultsForSecondCit()
        {
            var report1 = this.results.First(a => a.ReportTitle.DisplayValue == "D Name");
            report1.Rows.Should().HaveCount(1);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Part Number")).Should().Be("p1");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Description")).Should().Be("p1 desc");
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Qty")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Total Value")).Should().Be(808.08m);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("User Name")).Should().Be("Person 1");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Storage Place")).Should().Be("Store 34");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Supplier Id")).Should().Be("1");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Supplier Name")).Should().Be("s1");
        }
    }
}