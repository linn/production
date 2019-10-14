namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMCDReport : ContextBase
    {
        private DateRequestResource resource;

        private IResult<ManufacturingCommitDateResults> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new DateRequestResource { Date = 1.May(2020).ToString("o") };
            this.OrdersReports.ManufacturingCommitDate(this.resource.Date)
                .Returns(new ManufacturingCommitDateResults { IncompleteLinesAnalysis = new ResultsModel { ReportTitle = new NameModel("name") } });
            this.result = this.Sut.ManufacturingCommitDateReport(this.resource.Date);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.OrdersReports.Received().ManufacturingCommitDate(this.resource.Date);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ManufacturingCommitDateResults>>();
            var dataResult = ((SuccessResult<ManufacturingCommitDateResults>)this.result).Data;
            dataResult.IncompleteLinesAnalysis.ReportTitle.DisplayValue.Should().Be("name");
        }
    }
}