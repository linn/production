namespace Linn.Production.Domain.Tests.PartsReportSpecs
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartFailDetailsReport : ContextBase
    {
        private ResultsModel result;

        private string fromDate;

        private string toDate;

        [SetUp]
        public void SetUp()
        {
            this.fromDate = new DateTime(2019, 10, 01).ToString("o");

            this.toDate = new DateTime(2019, 10, 31).ToString("o");

            this.PartFailLogRepository.FilterBy(Arg.Any<Expression<Func<PartFailLog, bool>>>())
                .Returns(this.PartFailLogs.AsQueryable());

            this.LinnWeekPack.Wwsyy(DateTime.Parse(this.fromDate)).Returns("12/3");

            this.LinnWeekPack.Wwsyy(DateTime.Parse(this.toDate)).Returns("32/1");

            this.PartRepository.FilterBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(this.Parts.AsQueryable());

            this.result = this.Sut.PartFailDetailsReport(null, this.fromDate, this.toDate, "All", "All", "All", "All");
        }

        [Test]
        public void ShouldCallPartFailLogRepository()
        {
            this.PartFailLogRepository.Received().FilterBy(Arg.Any<Expression<Func<PartFailLog, bool>>>());
        }

        [Test]
        public void ShouldCallPartRepository()
        {
            this.PartRepository.Received().FilterBy(Arg.Any<Expression<Func<Part, bool>>>());
        }

        [Test]
        public void ShouldGetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Part Fail - Details for weeks 12/3 - 32/1");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(4);
        }
    }
}