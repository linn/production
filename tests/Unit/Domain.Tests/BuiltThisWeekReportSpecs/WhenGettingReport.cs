namespace Linn.Production.Domain.Tests.BuiltThisWeekReportSpecs
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

    public class WhenGettingReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var statistics = new List<BuiltThisWeekStatistic>()
            {
                new BuiltThisWeekStatistic { PartNumber="KLI SYS HUB", Description = "KLIMAX SYSTEM HUB", BuiltThisWeek = 1, Value = 977, Days=0.13, CitCode = "S", CitName = "FinalAssembly"},
                new BuiltThisWeekStatistic { PartNumber="LP12 OIL KIT", Description = "OIL BOTTLE", BuiltThisWeek = 36, Value = 9, Days=0.44, CitCode = "S", CitName = "FinalAssembly"}
            };
            this.BuiltThisWeekStatisticRepository.FilterBy(Arg.Any<Expression<Func<BuiltThisWeekStatistic, bool>>>())
                .Returns(statistics.AsQueryable());

            this.result = this.Sut.GetBuiltThisWeekReport("S");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Built this Week Detail");
        }

        [Test]
        public void ShouldHaveCorrectRows()
        {
            this.result.Rows.Should().HaveCount(2);
        }
    }
}
