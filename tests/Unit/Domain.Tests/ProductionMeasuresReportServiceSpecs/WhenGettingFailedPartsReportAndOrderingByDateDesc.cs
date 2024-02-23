namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFailedPartsReportAndOrderingByDateDesc : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.results = this.Sut.FailedPartsReport(null, null, "DESC", false, null, null);
        }

        [Test]
        public void ShouldGetData()
        {
            this.FailedPartsRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnOneReport()
        {
            this.results.Should().HaveCount(1);
        }

        [Test]
        public void ShouldReturnResultsDateDescending()
        {
            var report1 = this.results.First();
            report1.Rows.Should().HaveCount(4);

            report1.GetGridTextValue(report1.RowIndex("0"), report1.ColumnIndex("Date Booked")).Should().Be("01-Aug-2021");
            report1.GetGridTextValue(report1.RowIndex("1"), report1.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report1.GetGridTextValue(report1.RowIndex("2"), report1.ColumnIndex("Date Booked")).Should().Be("01-Jul-2021");
            report1.GetGridTextValue(report1.RowIndex("3"), report1.ColumnIndex("Date Booked")).Should().Be("01-Jul-2019");
        }
    }
}
