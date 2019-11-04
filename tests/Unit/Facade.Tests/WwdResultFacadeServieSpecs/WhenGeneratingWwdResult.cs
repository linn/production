namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGeneratingWwdResult : ContextBase
    {
        private IResult<WwdResult> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTriggerLevel
            {
                PartNumber = "AKUB PARTY",
                WsName = "AKUBSTATION"
            };
            this.ProductionTriggerLevelRepository.FindById(Arg.Any<string>()).Returns(trigger);
            this.WwdTrigFunction.WwdTriggerRun(Arg.Any<string>(), Arg.Any<int>()).Returns(1);
            this.result = this.Sut.GenerateWwdResultForTrigger("AKUB PARTY", 1, string.Empty);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<WwdResult>>();
        }

        [Test]
        public void ShouldCallWwdTrigFunction()
        {
            this.WwdTrigFunction.Received().WwdTriggerRun(Arg.Any<string>(), Arg.Any<int>());
            this.result.As<SuccessResult<WwdResult>>().Data.WwdJobId.Should().Be(1);
        }
    }
}