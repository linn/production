namespace Linn.Production.Facade.Tests.WhoBuiltWhatFacadeSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReport : ContextBase
    {
        private WhoBuiltWhatRequestResource resource;

        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WhoBuiltWhatRequestResource
                                {
                                    FromDate = 1.May(2020).ToString("o"),
                                    ToDate = 31.May(2020).ToString("o"),
                                    userNumber = 808,
                                    CitCode = "S"
                                };
            this.WhoBuiltWhatReport.WhoBuiltWhatDetails(this.resource.FromDate, this.resource.ToDate, this.resource.userNumber)
                .Returns(new ResultsModel { ReportTitle = new NameModel("name") });
            this.result = this.Sut.WhoBuiltWhatDetails(this.resource.FromDate, this.resource.ToDate, this.resource.userNumber);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.WhoBuiltWhatReport.Received().WhoBuiltWhatDetails(this.resource.FromDate, this.resource.ToDate, this.resource.userNumber);
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