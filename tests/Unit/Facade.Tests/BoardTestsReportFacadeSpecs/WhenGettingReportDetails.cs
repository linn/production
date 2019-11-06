namespace Linn.Production.Facade.Tests.BoardTestsReportFacadeSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReportDetails : ContextBase
    {
        private BoardTestRequestResource resource;

        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new BoardTestRequestResource
                                {
                                    BoardId = "AB12"
                                };
            this.BoardTestReports.GetBoardTestDetailsReport("AB12")
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.GetBoardTestDetailsReport(this.resource.BoardId);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.BoardTestReports.Received().GetBoardTestDetailsReport("AB12");
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