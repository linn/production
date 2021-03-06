﻿namespace Linn.Production.Facade.Tests.BoardTestsReportFacadeSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private BoardTestRequestResource resource;

        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new BoardTestRequestResource
                                {
                                    FromDate = 1.May(2020).ToString("o"),
                                    ToDate = 31.May(2020).ToString("o"),
                                    BoardId = "AB12"
                                };
            this.BoardTestReports.GetBoardTestReport(1.May(2020), 31.May(2020), "AB12")
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetBoardTestReport(
                this.resource.FromDate,
                this.resource.ToDate,
                this.resource.BoardId);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.BoardTestReports.Received().GetBoardTestReport(1.May(2020), 31.May(2020), "AB12");
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