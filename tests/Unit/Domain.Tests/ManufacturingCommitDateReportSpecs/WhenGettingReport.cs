namespace Linn.Production.Domain.Tests.ManufacturingCommitDateReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private ManufacturingCommitDateResults results;

        [SetUp]
        public void SetUp()
        {
            this.MCDLinesRepository.FilterBy(Arg.Any<Expression<Func<MCDLine, bool>>>())
                .Returns(
                    new List<MCDLine>
                                 {
                                     new MCDLine { ArticleNumber = "a", OrderNumber = 1, OrderLine = 1, CouldGo = 0, QtyOrdered = 4, Invoiced = 4, CoreType = "c", OrderLineCompleted = 1 },
                                     new MCDLine { ArticleNumber = "b", OrderNumber = 2, OrderLine = 1, CouldGo = 4, QtyOrdered = 4, Invoiced = 0, CoreType = "c", OrderLineCompleted = 0, Reason = "A SIF issue and SH issue" },
                                     new MCDLine { ArticleNumber = "v", OrderNumber = 3, OrderLine = 1, CouldGo = 0, QtyOrdered = 1, Invoiced = 0, CoreType = "c", OrderLineCompleted = 0, Reason = "a No Stock problem" },
                                     new MCDLine { ArticleNumber = "f", OrderNumber = 4, OrderLine = 1, CouldGo = 0, QtyOrdered = 3, Invoiced = 3, CoreType = "b", OrderLineCompleted = 1 },
                                     new MCDLine { ArticleNumber = "h", OrderNumber = 5, OrderLine = 1, CouldGo = 0, QtyOrdered = 1, Invoiced = 1, CoreType = "b", OrderLineCompleted = 1 }
                                 }.AsQueryable());
            this.results = this.Sut.ManufacturingCommitDate(1.May(2020).ToString("o"));
        }

        [Test]
        public void ShouldGetData()
        {
            this.MCDLinesRepository.Received().FilterBy(Arg.Any<Expression<Func<MCDLine, bool>>>());
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Results.Should().HaveCount(2);
        }

        [Test]
        public void ShouldIncludeSummaryValues()
        {
            var section1 = this.results.Results.First(a => a.ProductType == "c");
            section1.NumberOfLines.Should().Be(3);
            section1.NumberAvailable.Should().Be(2);
            section1.NumberSupplied.Should().Be(1);
            section1.PercentageSupplied.Should().Be(33.3m);
            section1.PercentageAvailable.Should().Be(66.7m);
            var section2 = this.results.Results.First(a => a.ProductType == "b");
            section2.NumberOfLines.Should().Be(2);
            section2.NumberAvailable.Should().Be(2);
            section2.NumberSupplied.Should().Be(2);
            section2.PercentageSupplied.Should().Be(100m);
            section2.PercentageAvailable.Should().Be(100m);
        }

        [Test]
        public void ShouldIncludeDetails()
        {
            var details1 = this.results.Results.First(a => a.ProductType == "c").Results;
            details1.Rows.Should().HaveCount(3);
            details1.GetGridTextValue(details1.RowIndex("1/1"), details1.ColumnIndex("Order Number")).Should().Be("1");
            details1.GetGridTextValue(details1.RowIndex("1/1"), details1.ColumnIndex("Order Line")).Should().Be("1");
            details1.GetGridTextValue(details1.RowIndex("1/1"), details1.ColumnIndex("Article Number")).Should().Be("a");
            details1.GetGridValue(details1.RowIndex("1/1"), details1.ColumnIndex("Qty Invoiced")).Should().Be(4);
            details1.GetGridValue(details1.RowIndex("1/1"), details1.ColumnIndex("Qty Ordered")).Should().Be(4);
            details1.GetGridValue(details1.RowIndex("1/1"), details1.ColumnIndex("Qty Could Go")).Should().Be(0);
            details1.GetGridValue(details1.RowIndex("2/1"), details1.ColumnIndex("Qty Invoiced")).Should().Be(0);
            details1.GetGridValue(details1.RowIndex("2/1"), details1.ColumnIndex("Qty Ordered")).Should().Be(4);
            details1.GetGridValue(details1.RowIndex("2/1"), details1.ColumnIndex("Qty Could Go")).Should().Be(4);
            details1.GetGridValue(details1.RowIndex("3/1"), details1.ColumnIndex("Qty Invoiced")).Should().Be(0);
            details1.GetGridValue(details1.RowIndex("3/1"), details1.ColumnIndex("Qty Ordered")).Should().Be(1);
            details1.GetGridValue(details1.RowIndex("3/1"), details1.ColumnIndex("Qty Could Go")).Should().Be(0);

            var details2 = this.results.Results.First(a => a.ProductType == "b").Results;
            details2.Rows.Should().HaveCount(2);
            details2.GetGridTextValue(details2.RowIndex("4/1"), details2.ColumnIndex("Order Number")).Should().Be("4");
            details2.GetGridTextValue(details2.RowIndex("4/1"), details2.ColumnIndex("Order Line")).Should().Be("1");
            details2.GetGridTextValue(details2.RowIndex("4/1"), details2.ColumnIndex("Article Number")).Should().Be("f");
            details2.GetGridValue(details2.RowIndex("4/1"), details2.ColumnIndex("Qty Invoiced")).Should().Be(3);
            details2.GetGridValue(details2.RowIndex("4/1"), details2.ColumnIndex("Qty Ordered")).Should().Be(3);
            details2.GetGridValue(details2.RowIndex("4/1"), details2.ColumnIndex("Qty Could Go")).Should().Be(0);
            details2.GetGridValue(details2.RowIndex("5/1"), details2.ColumnIndex("Qty Invoiced")).Should().Be(1);
            details2.GetGridValue(details2.RowIndex("5/1"), details2.ColumnIndex("Qty Ordered")).Should().Be(1);
            details2.GetGridValue(details2.RowIndex("5/1"), details2.ColumnIndex("Qty Could Go")).Should().Be(0);
        }

        [Test]
        public void ShouldSetTotals()
        {
            this.results.Totals.NumberOfLines.Should().Be(5);
            this.results.Totals.NumberAvailable.Should().Be(4);
            this.results.Totals.NumberSupplied.Should().Be(3);
            this.results.Totals.PercentageSupplied.Should().Be(60m);
            this.results.Totals.PercentageAvailable.Should().Be(80m);
        }

        [Test]
        public void ShouldIncludeAnalysisValues()
        {
            var analysis = this.results.IncompleteLinesAnalysis;
            analysis.GetGridValue(analysis.RowIndex("No Stock"), analysis.ColumnIndex("Qty")).Should().Be(1);
            analysis.GetGridValue(analysis.RowIndex("No Stock"), analysis.ColumnIndex("%")).Should().Be(50m);
            analysis.GetGridValue(analysis.RowIndex("Supply In Full"), analysis.ColumnIndex("Qty")).Should().Be(0);
            analysis.GetGridValue(analysis.RowIndex("Supply In Full"), analysis.ColumnIndex("%")).Should().Be(0m);
            analysis.GetGridValue(analysis.RowIndex("Shipment Hold"), analysis.ColumnIndex("Qty")).Should().Be(1m);
            analysis.GetGridValue(analysis.RowIndex("Shipment Hold"), analysis.ColumnIndex("%")).Should().Be(50m);
        }
    }
}