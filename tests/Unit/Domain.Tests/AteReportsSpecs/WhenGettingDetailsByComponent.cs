﻿namespace Linn.Production.Domain.Tests.AteReportsSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;

    using NUnit.Framework;

    public class WhenGettingDetailsByComponent : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetDetailsReport(
                1.June(2020),
                30.June(2020),
                "SMT",
                "ATE",
                string.Empty,
                "comp 2",
                string.Empty);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("ATE Test Fails between 01-Jun-2020 and 30-Jun-2020 for component comp 2");
        }

        [Test]
        public void ShouldSetCorrectValues()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.Columns.Should().HaveCount(11);
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Test Id")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Board Part Number")).Should().Be("part 1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Operator")).Should().Be("Emp 1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Item")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Batch Number")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Circuit Ref")).Should().Be("circuit 12");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Part Number")).Should().Be("comp 2");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Fault Code")).Should().Be("fault 1");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Smt Or Pcb")).Should().Be("SMT");
            this.result.GetGridTextValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("DetailOperator")).Should().Be("Emp 2");
            this.result.GetGridValue(this.result.RowIndex("1/001"), this.result.ColumnIndex("Fails")).Should().Be(1);

            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Test Id")).Should().Be("4");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Board Part Number")).Should().Be("part 1");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Operator")).Should().Be("Emp 1");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Item")).Should().Be("1");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Batch Number")).Should().BeNull();
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Circuit Ref")).Should().Be("circuit 1");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Part Number")).Should().Be("comp 2");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Fault Code")).Should().Be("fault 2");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Smt Or Pcb")).Should().Be("SMT");
            this.result.GetGridTextValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("DetailOperator")).Should().Be(string.Empty);
            this.result.GetGridValue(this.result.RowIndex("4/001"), this.result.ColumnIndex("Fails")).Should().Be(2);
        }
    }
}
