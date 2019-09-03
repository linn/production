namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMeasures : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var assemblyFails = new List<AssemblyFail>
                                    {
                                        new AssemblyFail { Id = 1, DateTimeFound = 1.June(2020), BoardPartNumber = "Board 1", NumberOfFails = 1 },
                                        new AssemblyFail { Id = 2, DateTimeFound = 11.June(2020), BoardPartNumber = "Board 2", NumberOfFails = 2 },
                                    };
            var weeks = new List<LinnWeek>
                            {
                                new LinnWeek { LinnWeekNumber = 20, StartDate = 1.June(2020), EndDate = 6.June(2020) },
                                new LinnWeek { LinnWeekNumber = 21, StartDate = 7.June(2020), EndDate = 13.June(2020) },
                                new LinnWeek { LinnWeekNumber = 22, StartDate = 14.June(2020), EndDate = 20.June(2020) }
                            };
            this.AssemblyFailRepository.FilterBy(Arg.Any<Expression<Func<AssemblyFail, bool>>>())
                .Returns(assemblyFails.AsQueryable());

            this.LinnWeekService.GetWeeks(1.June(2020), 30.June(2020)).Returns(weeks.AsQueryable());

            this.result = this.Sut.GetAssemblyFailsMeasuresReport(
                1.June(2020),
                30.June(2020),
                AssemblyFailGroupBy.boardPartNumber);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Assembly Fails Measures");
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(4);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 1"), this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 1"), this.result.ColumnIndex("Total")).Should().Be(1);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 2"), this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedGridValue(this.result.RowIndex("Board 2"), this.result.ColumnIndex("Total")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("20")).Should().Be(1);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("21")).Should().Be(2);
            this.result.GetZeroPaddedTotalValue(this.result.ColumnIndex("Total")).Should().Be(3);
        }
    }
}
