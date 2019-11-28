namespace Linn.Production.Domain.Tests.BuildPlansReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildPlansReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.BuildPlanDetailsLineRepository.FilterBy(Arg.Any<Expression<Func<BuildPlanDetailsReportLine, bool>>>())
                .Returns(
                    new List<BuildPlanDetailsReportLine>
                        {
                            new BuildPlanDetailsReportLine { PartNumber = "MAJIK", LinnWeekNumber = 20, CitName = "c1" },
                            new BuildPlanDetailsReportLine { PartNumber = "KLIMAX", LinnWeekNumber = 21, CitName = "c2" },
                            new BuildPlanDetailsReportLine { PartNumber = "PCAS", LinnWeekNumber = 22, CitName = "c3" }
                        }.AsQueryable());

            this.LinnWeekService.GetWeeks(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(
                new List<LinnWeek>
                    {
                        new LinnWeek
                            {
                                LinnWeekNumber = 20, StartDate = 1.June(2020), EndDate = 6.June(2020), WWSYY = "25/20"
                            },
                        new LinnWeek
                            {
                                LinnWeekNumber = 21, StartDate = 7.June(2020), EndDate = 13.June(2020), WWSYY = "26/20"
                            },
                        new LinnWeek
                            {
                                LinnWeekNumber = 22, StartDate = 14.June(2020), EndDate = 20.June(2020), WWSYY = "27/20"
                            }
                    }.AsQueryable());

            this.LinnWeekService.GetWeek(Arg.Any<DateTime>()).Returns(
                new LinnWeek
                {
                    LinnWeekNumber = 20,
                    StartDate = 1.June(2020),
                    EndDate = 6.June(2020),
                    WWSYY = "25/20"
                });

            this.result = this.Sut.BuildPlansReport("MASTER", 16, "ALL");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Build Plan Report");
        }

        [Test]
        public void ShouldSetRows()
        {
            this.result.Rows.Should().HaveCount(3);
        }

        [Test]
        public void ShouldSetColumns()
        {
            this.result.Columns.Should().HaveCount(4);
        }
    }
}
