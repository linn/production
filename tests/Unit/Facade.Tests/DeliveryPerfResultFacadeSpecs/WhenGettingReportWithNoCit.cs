namespace Linn.Production.Facade.Tests.DeliveryPerfResultFacadeSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using NUnit.Framework;

    public class WhenGettingReportWithNoCit : ContextBase
    {
        private IResult<ResultsModel> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GenerateDelPerfSummaryForCit(string.Empty);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ResultsModel>>();
            ((BadRequestResult<ResultsModel>)this.result).Message.Should().Be("Must specify a cit code");
        }
    }
}