using FluentAssertions;
using Linn.Common.Facade;
using Linn.Production.Domain.LinnApps.SerialNumberIssue;
using Linn.Production.Resources;
using NSubstitute;
using NUnit.Framework;

namespace Linn.Production.Facade.Tests.SerialNumberIssueServiceSpecs
{
    public class WhenAddingSerialNumberIssue : ContextBase
    {
        private SerialNumberIssueResource resource;

        private IResult<SerialNumberIssue> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new SerialNumberIssueResource
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
        public void ShouldAddSerialNumberIssue()
        {
            this.SerialNumberIssueRepository.Received().Add(Arg.Any<SerialNumberIssue>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<SerialNumberIssue>>();
            var dataResult = ((CreatedResult<SerialNumberIssue>) this.result).Data;
            dataResult.SernosGroup.Should().Be("group");
            dataResult.ArticleNumber.Should().Be("art");
            dataResult.SerialNumber.Should().Be(123);
            dataResult.NewArticleNumber.Should().Be("newart");
            dataResult.NewSerialNumber.Should().Be(321);
        }
    }
}
