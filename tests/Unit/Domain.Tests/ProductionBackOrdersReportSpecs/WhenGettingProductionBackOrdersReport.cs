namespace Linn.Production.Domain.Tests.ProductionBackOrdersReportSpecs
{
    using FluentAssertions;

    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductionBackOrdersReport : ContextBase
    {
        private ResultsModel result;

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
        public void ShouldGetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Production Back Orders");
        }

        [Test]
        public void ShouldSetReportValues()
        {
            this.result.Rows.Should().HaveCount(2);
            this.result.GetGridTextValue(this.result.RowIndex("A"), this.result.ColumnIndex("Article Number"))
                .Should().Be("A");
            this.result.GetGridTextValue(this.result.RowIndex("A"), this.result.ColumnIndex("Description"))
                .Should().Be("A Desc");
            this.result.GetGridValue(this.result.RowIndex("A"), this.result.ColumnIndex("Order Qty"))
                .Should().Be(3);
            this.result.GetGridValue(this.result.RowIndex("A"), this.result.ColumnIndex("Order Value"))
                .Should().Be(400.34m);
            this.result.GetGridTextValue(this.result.RowIndex("A"), this.result.ColumnIndex("Oldest Date"))
                .Should().Be("01-Dec-2020");
            this.result.GetGridValue(this.result.RowIndex("A"), this.result.ColumnIndex("Can Build Qty"))
                .Should().Be(3);
            this.result.GetGridValue(this.result.RowIndex("A"), this.result.ColumnIndex("Can Build Value"))
                .Should().Be(400.34m);

            this.result.GetGridTextValue(this.result.RowIndex("B"), this.result.ColumnIndex("Article Number"))
                .Should().Be("B");
            this.result.GetGridTextValue(this.result.RowIndex("B"), this.result.ColumnIndex("Description"))
                .Should().Be("B Desc");
            this.result.GetGridValue(this.result.RowIndex("B"), this.result.ColumnIndex("Order Qty"))
                .Should().Be(
                    4);
            this.result.GetGridValue(this.result.RowIndex("B"), this.result.ColumnIndex("Order Value"))
                .Should().Be(2252.92m);
            this.result.GetGridTextValue(this.result.RowIndex("B"), this.result.ColumnIndex("Oldest Date"))
                .Should().Be("01-Jul-2020");
            this.result.GetGridValue(this.result.RowIndex("B"), this.result.ColumnIndex("Can Build Qty"))
                .Should().Be(2);
            this.result.GetGridValue(this.result.RowIndex("B"), this.result.ColumnIndex("Can Build Value"))
                .Should().Be(1126.46m);
        }
    }
}