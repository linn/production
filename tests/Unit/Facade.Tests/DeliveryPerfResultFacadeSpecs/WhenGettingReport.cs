namespace Linn.Production.Facade.Tests.DeliveryPerfResultFacadeSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.DeliveryPerformanceReportService.GetDeliveryPerformanceByPriority("S").Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GenerateDelPerfSummaryForCit("S");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.DeliveryPerformanceReportService.Received().GetDeliveryPerformanceByPriority("S");
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
