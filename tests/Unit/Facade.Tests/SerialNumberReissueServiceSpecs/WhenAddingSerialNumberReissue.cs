namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingSerialNumberReissue : ContextBase
    {
        private SerialNumberReissueResource resource;

        private IResult<SerialNumberReissue> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new SerialNumberReissueResource
                                {
                                    SernosGroup = "group",
                                    ArticleNumber = "art",
                                    SerialNumber = 123,
                                    NewArticleNumber = "newart",
                                    NewSerialNumber = 321
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddSerialNumberReissue()
        {
            this.SerialNumberReissueRepository.Received().Add(Arg.Any<SerialNumberReissue>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<SerialNumberReissue>>();
            var dataResult = ((CreatedResult<SerialNumberReissue>) this.result).Data;
            dataResult.SernosGroup.Should().Be("group");
            dataResult.ArticleNumber.Should().Be("art");
            dataResult.SerialNumber.Should().Be(123);
            dataResult.NewArticleNumber.Should().Be("newart");
            dataResult.NewSerialNumber.Should().Be(321);
        }
    }
}
