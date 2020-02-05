namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDaysRequiredForCit : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.results = this.Sut.DayRequiredReport("C");
        }

        [Test]
        public void ShouldGetData()
        {
            this.ProductionDaysRequiredRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Should().HaveCount(2);
            var report1 = this.results.First(a => a.ReportTitle.DisplayValue == "Priority 1");
            report1.Rows.Should().HaveCount(3);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Part Number")).Should().Be("p1");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Part Number")).Should().Be("p2");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Description")).Should().Be("p1 desc");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Description")).Should().Be("p2 desc");
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Qty Being Built")).Should().Be(1);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Qty Being Built")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("2"), report1.ColumnIndex("Qty Being Built")).Should().Be(3);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Build")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Build")).Should().Be(4);
            report1.GetGridValue(report1.RowIndex("2"), report1.ColumnIndex("Build")).Should().Be(6);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Can Build")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Can Build")).Should().Be(3);
            report1.GetGridValue(report1.RowIndex("2"), report1.ColumnIndex("Can Build")).Should().Be(5);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Effective Kanban")).Should().Be(2);
            report1.GetGridValue(report1.RowIndex("1"), report1.ColumnIndex("Effective Kanban")).Should().Be(1);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Being Built Days")).Should().Be("1d 1h 36m");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Being Built Days")).Should().Be("1d 0h 0m");
            report1.GetGridTextValue(report1.RowIndex("2"), report1.ColumnIndex("Being Built Days")).Should().Be("2d 1h 36m");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Can Build Days")).Should().Be("2d 2h 21m");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Can Build Days")).Should().Be("4d 3h 7m");
            report1.GetGridTextValue(report1.RowIndex("2"), report1.ColumnIndex("Can Build Days")).Should().Be("6d 5h 28m");

            var report2 = this.results.First(a => a.ReportTitle.DisplayValue == "Priority 2");
            report2.Rows.Should().HaveCount(2);
            report2.GetGridTextValue(report2.RowIndex("0"), report2.ColumnIndex("Part Number")).Should().Be("p3");
            report2.GetGridTextValue(report2.RowIndex("1"), report2.ColumnIndex("Part Number")).Should().Be("Totals");
            report2.GetGridTextValue(report2.RowIndex("0"), report2.ColumnIndex("Description")).Should().Be("p3 desc");
            report2.GetGridValue(report2.RowIndex("0"), report2.ColumnIndex("Qty Being Built")).Should().Be(1);
            report2.GetGridValue(report2.RowIndex("1"), report2.ColumnIndex("Qty Being Built")).Should().Be(1);
            report2.GetGridValue(report2.RowIndex("0"), report2.ColumnIndex("Build")).Should().Be(2);
            report2.GetGridValue(report2.RowIndex("1"), report2.ColumnIndex("Build")).Should().Be(2);
            report2.GetGridValue(report2.RowIndex("0"), report2.ColumnIndex("Can Build")).Should().Be(2);
            report2.GetGridValue(report2.RowIndex("1"), report2.ColumnIndex("Can Build")).Should().Be(2);
            report2.GetGridValue(report2.RowIndex("0"), report2.ColumnIndex("Effective Kanban")).Should().Be(2);
            report2.GetGridTextValue(report2.RowIndex("0"), report2.ColumnIndex("Being Built Days")).Should().Be("1d 0h 0m");
            report2.GetGridTextValue(report2.RowIndex("1"), report2.ColumnIndex("Being Built Days")).Should().Be("1d 0h 0m");
            report2.GetGridTextValue(report2.RowIndex("0"), report2.ColumnIndex("Can Build Days")).Should().Be("2d 0h 0m");
            report2.GetGridTextValue(report2.RowIndex("1"), report2.ColumnIndex("Can Build Days")).Should().Be("2d 0h 0m");
        }
    }
}