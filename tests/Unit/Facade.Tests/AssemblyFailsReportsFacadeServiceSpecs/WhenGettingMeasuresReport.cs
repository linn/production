namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMeasuresReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.ReportService.GetAssemblyFailsMeasuresReport(1.May(2020), 1.July(2020), AssemblyFailGroupBy.partNumber)
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetAssemblyFailsMeasuresReport(1.May(2020).ToString("O"), 1.July(2020).ToString("O"));
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetAssemblyFailsMeasuresReport(1.May(2020), 1.July(2020), AssemblyFailGroupBy.partNumber);
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