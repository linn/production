namespace Linn.Production.Facade.Tests.DeliveryPerfResultFacadeSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingDetail: ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.DeliveryPerformanceReportService.GetDeliveryPerformanceDetail("S", 1).Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetDelPerfDetail("S", 1);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.DeliveryPerformanceReportService.Received().GetDeliveryPerformanceDetail("S", 1);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResultsModel>>();
            var dataResult = ((SuccessResult<ResultsModel>)this.result).Data;
            dataResult.ReportTitle.DisplayValue.Should().Be("name");
        }
    }
}