namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new AssemblyFailsDetailsReportRequestResource
                               {
                                   FromDate = 1.May(2020).ToString("o"),
                                   ToDate = 1.July(2020).ToString("o"),
                                   BoardPartNumber = "BP2",
                                   CircuitPartNumber = "CP",
                                   FaultCode = "F",
                                   CitCode = "CIT",
                                   Board = "BP2",
                                   Person = 1
                               };
            this.ReportService.GetAssemblyFailsDetailsReport(
                    1.May(2020),
                    1.July(2020),
                    "BP2",
                    "CP",
                    "F",
                    "CIT",
                    "BP2",
                    1)
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetAssemblyFailsDetailsReport(resource);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.ReportService.Received().GetAssemblyFailsDetailsReport(
                1.May(2020),
                1.July(2020),
                "BP2",
                "CP",
                "F",
                "CIT",
                "BP2",
                1);
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