using System;
using System.Linq.Expressions;

namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberReissue;
    using Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingSerialNumberReissue : ContextBase
    {
        private SerialNumberReissueResource resource;

        private SerialNumberReissue serialNumberReissue;

        private IResult<SerialNumberReissue> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new SerialNumberReissueResource
                                {
                                    SernosGroup = "group",
                                    ArticleNumber = "art",
                                    SerialNumber = 123,
                                    NewArticleNumber = "newart"
                                };

            this.serialNumberReissue = new SerialNumberReissue("group", "art")
            {
                NewSerialNumber = 1234,
                NewArticleNumber = "newart",
                Id = 111,
                SerialNumber = 123
            };

            this.SernosRenumPack.ReissueSerialNumber(Arg.Any<SerialNumberReissueResource>()).Returns(true);

            this.SerialNumberResissueRepository.FindBy(Arg.Any<Expression<Func<SerialNumberReissue, bool>>>())
                .Returns(this.serialNumberReissue);

            this.result = this.Sut.ReissueSerialNumber(this.resource);
        }

        [Test]
        public void ShouldCallSernosReissuePack()
        {
            this.SernosRenumPack.Received().ReissueSerialNumber(Arg.Any<SerialNumberReissueResource>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<SerialNumberReissue>>();
            var dataResult = ((CreatedResult<SerialNumberReissue>) this.result).Data;
            dataResult.SernosGroup.Should().Be("group");
            dataResult.ArticleNumber.Should().Be("art");
            dataResult.SerialNumber.Should().Be(123);
            dataResult.NewSerialNumber.Should().Be(1234);
            dataResult.NewArticleNumber.Should().Be("newart");
        }
    }
}
