namespace Linn.Production.Facade.Tests.AteReportsServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStatusReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.AteReportsService.GetStatusReport(
                    1.May(2020),
                    1.July(2020),
                    "smt",
                    "ate")
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetStatusReport(
                1.May(2020).ToString("o"),
                1.July(2020).ToString("o"),
                "smt",
                "ate");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.AteReportsService.Received().GetStatusReport(
                1.May(2020),
                1.July(2020),
                "smt",
                "ate");
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