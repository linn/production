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

    public class WhenGettingReport : ContextBase
    {
        private WhoBuiltWhatRequestResource resource;

        private IResult<IEnumerable<ResultsModel>> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WhoBuiltWhatRequestResource
                                {
                                    FromDate = 1.May(2020).ToString("o"),
                                    ToDate = 31.May(2020).ToString("o"),
                                    CitCode = "S"
                                };
            this.WhoBuiltWhatReport.WhoBuiltWhat(this.resource.FromDate, this.resource.ToDate, this.resource.CitCode)
                .Returns(new List<ResultsModel> { new ResultsModel { ReportTitle = new NameModel("name") } });
            this.result = this.Sut.WhoBuiltWhat(this.resource.FromDate, this.resource.ToDate, this.resource.CitCode);
        }

        [Test]
        public void ShouldGetReport()
        {
            this.WhoBuiltWhatReport.Received().WhoBuiltWhat(this.resource.FromDate, this.resource.ToDate, this.resource.CitCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<ResultsModel>>>();
            var dataResult = ((SuccessResult<IEnumerable<ResultsModel>>)this.result).Data.ToList();
            dataResult.Should().HaveCount(1);
            dataResult.First().ReportTitle.DisplayValue.Should().Be("name");
        }
    }
}