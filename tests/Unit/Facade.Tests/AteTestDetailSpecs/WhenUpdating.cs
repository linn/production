namespace Linn.Production.Facade.Tests.AteTestDetailSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private AteTestDetailResource updateResource;

        private IResult<AteTestDetail> result;

        [SetUp]
        public void SetUp()
        {
            this.updateResource = new AteTestDetailResource
                                      {
                                          ItemNumber = 1,
                                          PartNumber = "PART2",
                                          TestId = 1,
                                      };
            this.AteTestDetailRepository.FindById(Arg.Any<AteTestDetailKey>())
                .Returns(new AteTestDetail { ItemNumber = 1, TestId = 1, PartNumber = "PART" });
            this.result = this.Sut.Update(new AteTestDetailKey { ItemNumber = 1, TestId = 1 }, this.updateResource);
        }

        [Test]
        public void ShouldGetTestDetail()
        {
            this.AteTestDetailRepository.Received().FindById(Arg.Any<AteTestDetailKey>());
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            var dataResult = ((SuccessResult<AteTestDetail>)this.result).Data;
            dataResult.PartNumber.Should().Be("PART2");
        }
    }
}