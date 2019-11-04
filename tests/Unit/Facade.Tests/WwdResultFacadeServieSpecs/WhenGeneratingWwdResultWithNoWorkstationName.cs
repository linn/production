namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGeneratingWwdResultWithNoWorkstationName : ContextBase
    {
        private IResult<WwdResult> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTriggerLevel
            {
                PartNumber = "AKUB PARTY",
                WsName = string.Empty
            };
            this.ProductionTriggerLevelRepository.FindById(Arg.Any<string>()).Returns(trigger);
            this.result = this.Sut.GenerateWwdResultForTrigger("AKUB PARTY", 1, string.Empty);
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.result.Should().BeOfType<NotFoundResult<WwdResult>>();
            this.result.As<NotFoundResult<WwdResult>>().Message.Should().Be("No work station found");
        }
    }
}