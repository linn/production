namespace Linn.Production.Domain.Tests.PartsReportSpecs
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartFailDetailsReportWithCost : ContextBase
    {
        private ResultsModel result;

        private string fromDate;

        private string toDate;

        [SetUp]
        public void SetUp()
        {
            this.fromDate = new DateTime(2023, 04, 01).ToString("o");

            this.toDate = new DateTime(2023, 04, 05).ToString("o");

            this.PartFailLogRepository.FilterBy(Arg.Any<Expression<Func<PartFail, bool>>>())
                .Returns(this.PartFailLogs.AsQueryable());

            this.result = this.Sut.PartFailDetailsReport(null, this.fromDate, this.toDate, "All", "All", "All", "All");
        }

        [Test]
        public void ShouldCallPartFailLogRepository()
        {
            this.PartFailLogRepository.Received().FilterBy(Arg.Any<Expression<Func<PartFail, bool>>>());
        }

        [Test]
        public void ShouldCallPurchaseOrderRepository()
        {
            this.PurchaseOrderRepository.Received().FilterBy(Arg.Any<Expression<Func<PurchaseOrder, bool>>>());
        }

        [Test]
        public void ShouldCallSupplierRepository()
        {
            this.SupplierRepository.Received().FilterBy(Arg.Any<Expression<Func<Supplier, bool>>>());
        }

        [Test]
        public void ShouldGetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Part Fail - Details for dates 01/04/23 - 05/04/23");
        }

        [Test]
        public void ShouldSetColumnValues()
        {
            this.result.Rows.Should().HaveCount(4);

            // total price = base unit price * qty 
            this.result.GetGridValue(this.result.RowIndex("0"), this.result.ColumnIndex("Total Price")).Should().Be(3);
            this.result.GetGridValue(this.result.RowIndex("0"), this.result.ColumnIndex("Base Unit Price")).Should().Be(3);
            this.result.GetGridValue(this.result.RowIndex("0"), this.result.ColumnIndex("Quantity")).Should().Be(1);

            this.result.GetGridValue(this.result.RowIndex("1"), this.result.ColumnIndex("Total Price")).Should().Be(4);
            this.result.GetGridValue(this.result.RowIndex("1"), this.result.ColumnIndex("Base Unit Price")).Should().Be(4);
            this.result.GetGridValue(this.result.RowIndex("1"), this.result.ColumnIndex("Quantity")).Should().Be(1);

            // total price = base unit price * qty but No Cost set for next two rows so no price shown
            this.result.GetGridValue(this.result.RowIndex("2"), this.result.ColumnIndex("Total Price")).Should().Be(0);
            this.result.GetGridValue(this.result.RowIndex("2"), this.result.ColumnIndex("Base Unit Price")).Should().Be(5);
            this.result.GetGridValue(this.result.RowIndex("2"), this.result.ColumnIndex("Quantity")).Should().Be(1);

            this.result.GetGridValue(this.result.RowIndex("3"), this.result.ColumnIndex("Total Price")).Should().Be(0);
            this.result.GetGridValue(this.result.RowIndex("3"), this.result.ColumnIndex("Base Unit Price")).Should().Be(6);
            this.result.GetGridValue(this.result.RowIndex("3"), this.result.ColumnIndex("Quantity")).Should().Be(1);
        }
    }
}
