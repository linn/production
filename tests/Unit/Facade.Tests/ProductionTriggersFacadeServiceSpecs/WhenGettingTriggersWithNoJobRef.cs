namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTriggersWithNoJobRef : ContextBase
    {
        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            var cit = new Cit { Code = "S", Name = "Super Team" };
            this.CitRepository.FindById(Arg.Any<string>()).Returns(cit);

            this.PtlMasterRepository.GetRecord().Returns(new PtlMaster { LastFullRunJobref = "AAAAAA" });
            this.result = this.Sut.GetProductionTriggerReport(string.Empty, "S");
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggersReport>>();
        }

        [Test]
        public void ShouldHaveGotJobrefFromMaster()
        {
            this.PtlMasterRepository.Received().GetRecord();
        }
    }
}