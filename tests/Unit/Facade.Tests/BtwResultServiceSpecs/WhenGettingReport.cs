namespace Linn.Production.Facade.Tests.BtwResultServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.BuiltThisWeekReportService.GetBuiltThisWeekReport("S").Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GenerateBtwResultForCit("S");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.BuiltThisWeekReportService.Received().GetBuiltThisWeekReport("S");
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
