namespace Linn.Production.Facade.Tests.LabelReprintFacadeServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private LabelReprintResource resource;

        private IResult<LabelReprint> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LabelReprintResource
                                {
                                    ReprintType = "big",
                                    LabelTypeCode = "code",
                                    Links = new[] { new LinkResource("requested-by", "/employees/1234") }
                                };
            this.LabelService.CreateLabelReprint(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Is<string>(s => s == "big"),
                    Arg.Any<string>())
                .Returns(new LabelReprint { LabelReprintId = 808 });
            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LabelService.Received().CreateLabelReprint(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Is<string>(s => s == "big"),
                Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<LabelReprint>>();
            var data = ((CreatedResult<LabelReprint>)this.result).Data;
            data.LabelReprintId.Should().Be(808);
        }
    }
}