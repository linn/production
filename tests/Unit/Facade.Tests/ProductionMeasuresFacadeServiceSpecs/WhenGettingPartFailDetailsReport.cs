namespace Linn.Production.Facade.Tests.ProductionMeasuresFacadeServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFailsReport : ContextBase
    {
        private IResult<IEnumerable<ResultsModel>> results;

        [SetUp]
        public void SetUp()
        {
            this.ProductionMeasuresReportService.FailedPartsReport("P", null, null, false, null, null)
                .Returns(new List<ResultsModel> { new ResultsModel { ReportTitle = new NameModel("Title") } });

            this.results = this.Sut.GetFailedPartsReport("P", null, null, false, null, null);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ProductionMeasuresReportService.Received().FailedPartsReport("P", null, null, false, null, null);
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