namespace Linn.Production.Facade.Tests.PtlSettingsFacadeServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PtlSettingsResource resource;

        private PtlSettings settings;

        private IResult<PtlSettings> result;

        [SetUp]
        public void SetUp()
        {
            this.settings = new PtlSettings();
            this.resource = new PtlSettingsResource
                                {
                                    BuildToMonthEndFromDays = 1,
                                    DaysToLookAhead = 2,
                                    FinalAssemblyDaysToLookAhead = 3,
                                    PriorityCutOffDays = 4,
                                    PriorityStrategy = 5,
                                    SubAssemblyDaysToLookAhead = 6
                                };

            this.PtlSettingsRepository.GetRecord()
                .Returns(this.settings);
            this.result = this.Sut.Update(this.resource);
        }

        [Test]
        public void ShouldGetRecord()
        {
            this.PtlSettingsRepository.Received().GetRecord();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<PtlSettings>>();
            var dataResult = ((SuccessResult<PtlSettings>)this.result).Data;
            dataResult.BuildToMonthEndFromDays.Should().Be(1);
            dataResult.DaysToLookAhead.Should().Be(2);
            dataResult.FinalAssemblyDaysToLookAhead.Should().Be(3);
            dataResult.PriorityCutOffDays.Should().Be(4);
            dataResult.PriorityStrategy.Should().Be(5);
            dataResult.SubAssemblyDaysToLookAhead.Should().Be(6);
        }
    }
}