namespace Linn.Production.Facade.Tests.ProductionMeasuresFacadeServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDaysRequiredReport : ContextBase
    {
        private IResult<IEnumerable<ResultsModel>> results;

        [SetUp]
        public void SetUp()
        {
            this.ProductionMeasuresReportService.DayRequiredReport("P").Returns(
                new List<ResultsModel> { new ResultsModel { ReportTitle = new NameModel("Title") } });

            this.results = this.Sut.GetDaysRequiredReport("P");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ProductionMeasuresReportService.Received().DayRequiredReport("P");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ResultsModel>>>();
            var dataResult = ((SuccessResult<IEnumerable<ResultsModel>>)this.results).Data;
            dataResult.First().ReportTitle.DisplayValue.Should().Be("Title");
        }
    }
}