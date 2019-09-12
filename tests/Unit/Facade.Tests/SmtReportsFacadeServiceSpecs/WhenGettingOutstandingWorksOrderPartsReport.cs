namespace Linn.Production.Facade.Tests.SmtReportsFacadeServiceSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrderPartsReport : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.SmtReportsService.OutstandingWorksOrderParts("SMT1", Arg.Any<string[]>())
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetPartsForOutstandingWorksOrders("SMT1", new[] { "P1" });
        }

        [Test]
        public void ShouldGetReport()
        {
            this.SmtReportsService.Received().OutstandingWorksOrderParts(
                "SMT1", Arg.Is<string[]>(s => s.Contains("P1")));
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