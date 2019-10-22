namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Resources;

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
                                    NewSerialNumber = 555,
                                    NewArticleNumber = "newart",
                                    Comments = "comment",
                                    CreatedBy = 33067,
                                    Links = new List<LinkResource>
                                                {
                                                    new LinkResource("created-by", "/employees/33067")
                                                }.ToArray()
                                };

            this.serialNumberReissue = new SerialNumberReissue("group", "art")
            {
                NewSerialNumber = 1234,
                NewArticleNumber = "newart",
                Id = 111,
                SerialNumber = 123
            };

            this.SernosRenumPack.ReissueSerialNumber(
                this.resource.SernosGroup,
                this.resource.SerialNumber,
                this.resource.NewSerialNumber,
                this.resource.ArticleNumber,
                this.resource.NewArticleNumber,
                this.resource.Comments,
                this.resource.CreatedBy).Returns("SUCCESS");

            this.SerialNumberResissueRepository.FindBy(Arg.Any<Expression<Func<SerialNumberReissue, bool>>>())
                .Returns(this.serialNumberReissue);

            this.result = this.Sut.ReissueSerialNumber(this.resource);
        }

        [Test]
        public void ShouldCallSernosReissuePack()
        {
            this.SernosRenumPack.Received().ReissueSerialNumber(
                this.resource.SernosGroup,
                this.resource.SerialNumber,
                this.resource.NewSerialNumber,
                this.resource.ArticleNumber,
                this.resource.NewArticleNumber,
                this.resource.Comments,
                this.resource.CreatedBy);
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
