namespace Linn.Production.Domain.Tests.ProductionBackOrdersReportSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductionBackOrdersReport : ContextBase
    {
        private IEnumerable<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.ProductionBackOrders(null);
        }

        [Test]
        public void ShouldCallAccountingCompanyRepository()
        {
            this.AccountingCompaniesRepository.Received().FindById("LINN");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Should().HaveCount(2);
            var cit1 = this.result.First();
            cit1.ReportTitle.DisplayValue.Should().Be("Cit S - Production");
            var cit2 = this.result.Last();
            cit2.ReportTitle.DisplayValue.Should().Be("Cit T - Turning");
            cit1.Rows.Should().HaveCount(2);
            cit1.GetGridTextValue(cit1.RowIndex("0"), cit1.ColumnIndex("Article Number"))
                .Should().Be("A");
            cit1.GetGridTextValue(cit1.RowIndex("0"), cit1.ColumnIndex("Description"))
                .Should().Be("A Desc");
            cit1.GetGridValue(cit1.RowIndex("0"), cit1.ColumnIndex("Order Qty"))
                .Should().Be(3);
            cit1.GetGridValue(cit1.RowIndex("0"), cit1.ColumnIndex("Order Value"))
                .Should().Be(400.34m);
            cit1.GetGridTextValue(cit1.RowIndex("0"), cit1.ColumnIndex("Oldest Date"))
                .Should().Be("01-Jul-2020");
            cit1.GetGridValue(cit1.RowIndex("0"), cit1.ColumnIndex("Can Build Qty"))
                .Should().Be(3);
            cit1.GetGridValue(cit1.RowIndex("0"), cit1.ColumnIndex("Can Build Value"))
                .Should().Be(400.34m);

            cit1.GetGridTextValue(cit1.RowIndex("1"), cit1.ColumnIndex("Article Number"))
                .Should().Be("B");
            cit1.GetGridTextValue(cit1.RowIndex("1"), cit1.ColumnIndex("Description"))
                .Should().Be("B Desc");
            cit1.GetGridValue(cit1.RowIndex("1"), cit1.ColumnIndex("Order Qty"))
                .Should().Be(
                    4);
            cit1.GetGridValue(cit1.RowIndex("1"), cit1.ColumnIndex("Order Value"))
                .Should().Be(2252.92m);
            cit1.GetGridTextValue(cit1.RowIndex("1"), cit1.ColumnIndex("Oldest Date"))
                .Should().Be("01-Dec-2020");
            cit1.GetGridValue(cit1.RowIndex("1"), cit1.ColumnIndex("Can Build Qty"))
                .Should().Be(2);
            cit1.GetGridValue(cit1.RowIndex("1"), cit1.ColumnIndex("Can Build Value"))
                .Should().Be(1126.46m);

            cit2.GetGridTextValue(cit2.RowIndex("0"), cit2.ColumnIndex("Article Number"))
                .Should().Be("C");
            cit2.GetGridTextValue(cit2.RowIndex("0"), cit2.ColumnIndex("Description"))
                .Should().Be("C Desc");
            cit2.GetGridValue(cit2.RowIndex("0"), cit2.ColumnIndex("Order Qty"))
                .Should().Be(
                    1);
            cit2.GetGridValue(cit2.RowIndex("0"), cit2.ColumnIndex("Order Value"))
                .Should().Be(100m);
            cit2.GetGridTextValue(cit2.RowIndex("0"), cit2.ColumnIndex("Oldest Date"))
                .Should().Be("01-Aug-2020");
            cit2.GetGridValue(cit2.RowIndex("0"), cit2.ColumnIndex("Can Build Qty"))
                .Should().Be(1);
            cit2.GetGridValue(cit2.RowIndex("0"), cit2.ColumnIndex("Can Build Value"))
                .Should().Be(100m);
        }
    }
}