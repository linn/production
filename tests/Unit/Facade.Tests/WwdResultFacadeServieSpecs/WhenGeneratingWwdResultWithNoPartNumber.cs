namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenGeneratingWwdResultWithNoPartNumber : ContextBase
    {
        private IResult<WwdResult> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GenerateWwdResultForTrigger(string.Empty, 1, string.Empty);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<WwdResult>>();
            this.result.As<BadRequestResult<WwdResult>>().Message.Should().Be("No part number supplied.");
        }
    }
}