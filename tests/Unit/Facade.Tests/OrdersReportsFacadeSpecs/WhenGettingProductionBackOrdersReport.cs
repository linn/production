namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductionBackOrdersReport : ContextBase
    {
        private IResult<IEnumerable<ResultsModel>> result;

        [SetUp]
        public void SetUp()
        {
            this.ProductionBackOrdersReportService.ProductionBackOrders("B")
                .Returns(new List<ResultsModel> { new ResultsModel { ReportTitle = new NameModel("Title") } });

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
            this.result.Should().BeOfType<SuccessResult<IEnumerable<ResultsModel>>>();
            var dataResult = ((SuccessResult<IEnumerable<ResultsModel>>)this.result).Data;
            dataResult.First().ReportTitle.DisplayValue.Should().Be("Title");
        }
    }
}