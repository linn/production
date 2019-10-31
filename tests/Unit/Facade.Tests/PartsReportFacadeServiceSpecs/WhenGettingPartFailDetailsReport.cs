namespace Linn.Production.Facade.Tests.PartsReportFacadeServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartFailDetailsReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.PartsReportService.PartFailDetailsReport(123, "fw", "tw", "et", "fc", "pn", "de")
                .Returns(new ResultsModel { ReportTitle = new NameModel("title") });

            this.result = this.Sut.GetPartFailDetailsReport(123, "fw", "tw", "et", "fc", "pn", "de");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.PartsReportService.Received().PartFailDetailsReport(123, "fw", "tw", "et", "fc", "pn", "de");
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