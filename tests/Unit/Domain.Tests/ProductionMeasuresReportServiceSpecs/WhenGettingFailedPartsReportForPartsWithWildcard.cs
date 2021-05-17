namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFailedPartsReportForAParWithWildcardt : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.results = this.Sut.FailedPartsReport(null, "*p1*", null);
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
            report.Rows.Should().HaveCount(3);
            report.GetGridTextValue(report.RowIndex("0"), report.ColumnIndex("Part Number")).Should().Be("p1");
            report.GetGridTextValue(report.RowIndex("1"), report.ColumnIndex("Part Number")).Should().Be("p1");
            report.GetGridTextValue(report.RowIndex("2"), report.ColumnIndex("Part Number")).Should().Be("variant of p1");
        }
    }
}
