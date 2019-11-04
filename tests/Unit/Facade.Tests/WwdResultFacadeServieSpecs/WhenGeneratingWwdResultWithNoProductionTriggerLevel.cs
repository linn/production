namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGeneratingWwdResultWithNoProductionTriggerLevel : ContextBase
    {
        private IResult<WwdResult> result;

        [SetUp]
        public void SetUp()
        {
            this.ProductionTriggerLevelRepository.FindById(Arg.Any<string>()).Returns((ProductionTriggerLevel) null);
            this.result = this.Sut.GenerateWwdResultForTrigger("AKUB PARTY", 1, string.Empty);
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.result.Should().BeOfType<NotFoundResult<WwdResult>>();
            this.result.As<NotFoundResult<WwdResult>>().Message.Should().Be("No production trigger level found");
        }
    }
}