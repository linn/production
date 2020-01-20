namespace Linn.Production.Domain.Tests.DeliveryPerformanceReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingDetail : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var statistics = new List<PtlStat>()
            {
                new PtlStat { PtlPriority = 1, WorkingDays = 10m, PartNumber = "LP12/M/O/50", TriggerId = 4, TriggerDate = new DateTime(2019,12,13), DateCompleted = new DateTime(2019,12,23)},
                new PtlStat { PtlPriority = 1, WorkingDays = 2.3m, PartNumber = "S3 302/C", TriggerId = 3, TriggerDate = new DateTime(2019,12,19), DateCompleted = new DateTime(2019,12,21)},
                new PtlStat { PtlPriority = 1, WorkingDays = 1, PartNumber = "TWIN/1/BP", TriggerId = 2, TriggerDate = new DateTime(2019,12,20), DateCompleted = new DateTime(2019,12,21)},
                new PtlStat { PtlPriority = 1, WorkingDays = 0.5m, PartNumber = "S3 301/C", TriggerId = 1, TriggerDate = new DateTime(2019,12,3), DateCompleted = new DateTime(2019,12,3)}
            };
            this.PtlStatRepository.FilterBy(Arg.Any<Expression<Func<PtlStat, bool>>>())
                .Returns(statistics.AsQueryable());

            this.LinnWeekService.LinnWeekStartDate(Arg.Any<DateTime>()).Returns(new DateTime(2019, 12, 14));
            this.LinnWeekService.LinnWeekEndDate(Arg.Any<DateTime>()).Returns(new DateTime(2020, 1, 10));

            this.result = this.Sut.GetDeliveryPerformanceDetail("S",1);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Production Delivery Performance 14-Dec-19 - 10-Jan-20 Priority 1 Cit S");
        }

        [Test]
        public void ShouldHaveOneRowPerStat()
        {
            this.result.Rows.Should().HaveCount(4);
        }

        [Test]
        public void ShouldBeInOrderOfWorkingDays()
        {
            this.result.RowId(0).Should().Be("4"); 
            this.result.RowId(1).Should().Be("3");  
            this.result.RowId(2).Should().Be("2");
            this.result.RowId(3).Should().Be("1");

            this.result.GetGridTextValue(0, this.result.ColumnIndex("workingDays")).Should().Be("10.0");
            this.result.GetGridTextValue(1, this.result.ColumnIndex("workingDays")).Should().Be("2.3");
            this.result.GetGridTextValue(2, this.result.ColumnIndex("workingDays")).Should().Be("1.0");
            this.result.GetGridTextValue(3, this.result.ColumnIndex("workingDays")).Should().Be("0.5");
        }
    }
}