namespace Linn.Production.Facade.Tests.AteReportsServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Facade.Extensions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.AteReportsService.GetDetailsReport(
                    1.May(2020),
                    1.July(2020),
                    "smt",
                    "ate",
                    "component".ParseAteReportOption(),
                    "value")
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetDetailsReport(
                1.May(2020).ToString("o"),
                1.July(2020).ToString("o"),
                "smt",
                "ate",
                "component",
                "value");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.AteReportsService.Received().GetDetailsReport(
                1.May(2020),
                1.July(2020),
                "smt",
                "ate",
                "component".ParseAteReportOption(),
                "value");
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
