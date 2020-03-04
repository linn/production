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

            this.PartFailLogRepository.FilterBy(Arg.Any<Expression<Func<PartFail, bool>>>())
                .Returns(this.PartFailLogs.AsQueryable());

            this.LinnWeekPack.Wwsyy(DateTime.Parse(this.fromDate)).Returns("12/3");

            this.LinnWeekPack.Wwsyy(DateTime.Parse(this.toDate)).Returns("32/1");

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
            this.result.ReportTitle.DisplayValue.Should().Be("Part Fail - Details for weeks 12/3 - 32/1");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(4);
        }

        [Test]
        public void ShouldSetColumnValues()
        {
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Part Number");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Part Description");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Date Created");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Batch");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Fault Code");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Story");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Quantity");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Minutes Wasted");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Error Type");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Base Unit Price");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Total Price");
            this.result.Columns.Should().Contain(c => c.ColumnHeader == "Entered By");
        }
    }
}