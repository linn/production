namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductionBackOrdersReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.ProductionBackOrdersReportService.ProductionBackOrders("B")
                .Returns(new ResultsModel { ReportTitle = new NameModel("Title") });

            this.result = this.Sut.ProductionBackOrdersReport("B");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ProductionBackOrdersReportService.Received().ProductionBackOrders("B");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.result).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be("Title");
        }
    }
}