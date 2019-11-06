namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOverdueOrdersReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.OverdueOrdersService.OverdueOrdersReport("RB", "DM")
                .Returns(new ResultsModel { ReportTitle = new NameModel("title") });

            this.result = this.Sut.GetOverdueOrdersReport("RB", "DM");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.OverdueOrdersService.Received().OverdueOrdersReport("RB", "DM");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.result).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be("title");
        }
    }
}