namespace Linn.Production.Facade.Tests.BoardFailTypesServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingBoardFailType : ContextBase
    {
        private BoardFailTypeResource resource;

        private IResult<BoardFailType> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new BoardFailTypeResource
                                {
                                    FailType = 1,
                                    Description = "Desc"
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddBoardFailType()
        {
            this.BoardFailTypeRepository.Received().Add(Arg.Any<BoardFailType>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<BoardFailType>>();
            var dataResult = ((CreatedResult<BoardFailType>)this.result).Data;
            dataResult.Type.Should().Be(1);
            dataResult.Description.Should().Be("Desc");
        }
    }
}