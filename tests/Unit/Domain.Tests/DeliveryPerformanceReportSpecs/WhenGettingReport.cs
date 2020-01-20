namespace Linn.Production.Domain.Tests.DeliveryPerformanceReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var statistics = new List<PtlStat>()
            {
                new PtlStat { PtlPriority = 1, WorkingDays = 0.5m},
                new PtlStat { PtlPriority = 1, WorkingDays = 1 },
                new PtlStat { PtlPriority = 1, WorkingDays = 2.3m },
                new PtlStat { PtlPriority = 1, WorkingDays = 10m },
                new PtlStat { PtlPriority = 2, WorkingDays = 4.1m },
                new PtlStat { PtlPriority = 2, WorkingDays = 4.1m }
            };
            this.PtlStatRepository.FilterBy(Arg.Any<Expression<Func<PtlStat, bool>>>())
                .Returns(statistics.AsQueryable());

            this.LinnWeekService.LinnWeekStartDate(Arg.Any<DateTime>()).Returns(new DateTime(2019, 12, 14));
            this.LinnWeekService.LinnWeekEndDate(Arg.Any<DateTime>()).Returns(new DateTime(2020, 1, 10));

            this.result = this.Sut.GetDeliveryPerformanceByPriority("S");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Production Delivery Performance 14-Dec-19 - 10-Jan-20");
        }

        [Test]
        public void ShouldHaveOneRowPerPriority()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("priority")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("priority")).Should().Be("2");
        }

        [Test]
        public void ShouldHaveCorrectNumofTriggersPerPriority()
        {
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("triggers")).Should().Be("4");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("triggers")).Should().Be("2");
        }

        [Test]
        public void ShouldHaveCorrectDayCountsPerPriority()
        {
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("1day")).Should().Be("2");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("2day")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("3day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("4day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("5day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("gt5day")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("1day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("2day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("3day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("4day")).Should().Be("2");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("5day")).Should().Be("0");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("gt5day")).Should().Be("0");
        }

        [Test]
        public void ShouldHaveCorrectAvgTurnaroundPerPriority()
        {
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("avgTurnaround")).Should().Be("3.4");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("avgTurnaround")).Should().Be("4.1");
        }

        [Test]
        public void ShouldHaveCorrectPercBy5Day()
        {
            this.result.GetGridTextValue(this.result.RowIndex("1"), this.result.ColumnIndex("percBy5days")).Should().Be("75");
            this.result.GetGridTextValue(this.result.RowIndex("2"), this.result.ColumnIndex("percBy5days")).Should().Be("100");
        }
    }
}
