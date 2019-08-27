namespace Linn.Production.Facade.Tests.BoardFailTypesServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingBoardFailType : ContextBase
    {
        private BoardFailTypeResource resource;

        private IResult<BoardFailType> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new BoardFailTypeResource
                                {
                                    FailType = 1,
                                    Description = "Desc",
                                };

            this.BoardFailTypeRepository.FindById(1)
                .Returns(new BoardFailType { Type = 1, Description = "Old Desc" });
            this.result = this.Sut.Update(1, this.resource);
        }

        [Test]
        public void ShouldGetBoardFailType()
        {
            this.BoardFailTypeRepository.Received().FindById(1);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<BoardFailType>>();
            var dataResult = ((SuccessResult<BoardFailType>)this.result).Data;
            dataResult.Type.Should().Be(1);
            dataResult.Description.Should().Be("Desc");
        }
    }
}