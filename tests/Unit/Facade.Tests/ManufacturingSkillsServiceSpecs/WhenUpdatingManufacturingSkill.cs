namespace Linn.Production.Facade.Tests.ManufacturingSkillServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Tests.ManufacturingSkillsServiceSpecs;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingManufacturingSkill : ContextBase
    {
        private ManufacturingSkillResource resource;

        private IResult<ManufacturingSkill> result;

        private ManufacturingSkill ManufacturingSkill;

        private string newDesc = "new description";

        private int newHourlyRate = 118;

        [SetUp]
        public void SetUp()
        {
            this.ManufacturingSkill = new ManufacturingSkill("skill1", "Descr", 15);

            this.resource = new ManufacturingSkillResource
            {
                SkillCode = this.ManufacturingSkill.SkillCode,
                Description = this.newDesc,
                HourlyRate = this.newHourlyRate,
            };

            this.ManufacturingSkillRepository.FindById(this.ManufacturingSkill.SkillCode)
                .Returns(this.ManufacturingSkill);
            this.result = this.Sut.Update(this.ManufacturingSkill.SkillCode, this.resource);
        }

        [Test]
        public void ShouldGetManufacturingSkill()
        {
            this.ManufacturingSkillRepository.Received().FindById(this.ManufacturingSkill.SkillCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ManufacturingSkill>>();
            var dataResult = ((SuccessResult<ManufacturingSkill>)this.result).Data;
            dataResult.SkillCode.Should().Be(this.ManufacturingSkill.SkillCode);
            dataResult.Description.Should().Be(this.newDesc);
            dataResult.HourlyRate.Should().Be(this.newHourlyRate);
        }
    }
}
