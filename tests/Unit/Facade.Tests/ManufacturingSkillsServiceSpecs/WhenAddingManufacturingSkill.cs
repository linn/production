namespace Linn.Production.Facade.Tests.ManufacturingSkillsServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingManufacturingSkill : ContextBase
    {
        private ManufacturingSkillResource resource;

        private IResult<ManufacturingSkill> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ManufacturingSkillResource
            {
                SkillCode = "skill",
                Description = "Desc",
                HourlyRate = 11
            };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddManufacturingSkill()
        {
            this.ManufacturingSkillRepository.Received().Add(Arg.Any<ManufacturingSkill>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<ManufacturingSkill>>();
            var dataResult = ((CreatedResult<ManufacturingSkill>)this.result).Data;
            dataResult.SkillCode.Should().Be("skill");
            dataResult.Description.Should().Be("Desc");
            dataResult.HourlyRate.Should().Be(11);
        }
    }
}
