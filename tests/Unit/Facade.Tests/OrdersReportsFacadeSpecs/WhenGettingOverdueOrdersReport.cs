namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOverdueOrdersReport : ContextBase
    {
        private IResult<ResultsModel> result;

        private string testDate;

        [SetUp]
        public void SetUp()
        {
            this.testDate = DateTime.UnixEpoch.ToString("d");

            this.OverdueOrdersService.OverdueOrdersReport(123, this.testDate, this.testDate, "AC", "SP", "RB", "DM")
                .Returns(new ResultsModel { ReportTitle = new NameModel("title") });

            this.result = this.Sut.GetOverdueOrdersReport(123, this.testDate, this.testDate, "AC", "SP", "RB", "DM");
        }

        [Test]
        public void ShouldGetReport()
        {
            this.OverdueOrdersService.Received().OverdueOrdersReport(123, this.testDate, this.testDate, "AC", "SP", "RB", "DM");
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