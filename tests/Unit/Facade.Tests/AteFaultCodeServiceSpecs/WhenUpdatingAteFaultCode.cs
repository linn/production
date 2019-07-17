namespace Linn.Production.Facade.Tests.AteFaultCodeServiceSpecs
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAteFaultCode : ContextBase
    {
        private AteFaultCodeResource resource;

        private IResult<AteFaultCode> result;

        private string ateFaultCode;

        [SetUp]
        public void SetUp()
        {
            this.ateFaultCode = "F1";

            this.resource = new AteFaultCodeResource
                                {
                                    FaultCode = this.ateFaultCode,
                                    Description = "Desc",
                                    DateInvalid = 31.July(2019).ToString("o")
                                };

            this.AteFaultCodeRepository.FindById(this.ateFaultCode)
                .Returns(new AteFaultCode(this.ateFaultCode));
            this.result = this.Sut.Update(this.ateFaultCode, this.resource);
        }

        [Test]
        public void ShouldGetAteFaultCode()
        {
            this.AteFaultCodeRepository.Received().FindById(this.ateFaultCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<AteFaultCode>>();
            var dataResult = ((SuccessResult<AteFaultCode>)this.result).Data;
            dataResult.FaultCode.Should().Be(this.ateFaultCode);
            dataResult.Description.Should().Be("Desc");
            dataResult.DateInvalid.Should().Be(31.July(2019));
        }
    }
}