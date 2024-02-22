namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReportForStockPool : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        private string stockPoolCode;

        [SetUp]
        public void SetUp()
        {
            this.stockPoolCode = "SPC 1";
            this.results = this.Sut.FailedPartsReport(null, null, null, false, null, this.stockPoolCode);
        }

        [Test]
        public void ShouldGetData()
        {
            this.FailedPartsRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Should().HaveCount(1);
            var report1 = this.results.First(a => a.ReportTitle.DisplayValue == "C Name");
            report1.Rows.Should().HaveCount(1);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Part Number")).Should().Be("p1");
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Description")).Should().Be("p1 desc");
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Qty")).Should().Be(2.5m);
            report1.GetGridValue(report1.RowIndex("0"), report1.ColumnIndex("Total Value")).Should().Be(34.34m);
            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Stock Pool")).Should().Be(this.stockPoolCode);
        }
    }
}
