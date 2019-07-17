namespace Linn.Production.Facade.Tests.AteFaultCodeServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingAteFaultCode : ContextBase
    {
        private AteFaultCodeResource resource;

        private IResult<AteFaultCode> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new AteFaultCodeResource
                                {
                                    FaultCode = "F",
                                    Description = "Desc"
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddAteFaultCode()
        {
            this.AteFaultCodeRepository.Received().Add(Arg.Any<AteFaultCode>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<AteFaultCode>>();
            var dataResult = ((CreatedResult<AteFaultCode>)this.result).Data;
            dataResult.FaultCode.Should().Be("F");
            dataResult.Description.Should().Be("Desc");
            dataResult.DateInvalid.Should().BeNull();
        }
    }
}