namespace Linn.Production.Facade.Tests.ManufacturingSkillsServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Microsoft.EntityFrameworkCore.Query.Internal;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingManufacturingSkill : ContextBase
    {
        private const string NewDesc = "new description";

        private const int NewHourlyRate = 118;

        private ManufacturingSkillResource resource;

        private IResult<ManufacturingSkill> result;

        private ManufacturingSkill manufacturingSkill;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingSkill = new ManufacturingSkill
                                          {
                                              SkillCode = "skill1",
                                              Description = "Descr",
                                              HourlyRate = 15,
                                              DateInvalid = null
                                          };

            this.resource = new ManufacturingSkillResource
            {
                SkillCode = this.manufacturingSkill.SkillCode,
                Description = NewDesc,
                HourlyRate = NewHourlyRate,
                DateInvalid = DateTime.Today.ToString("o")
            };

            this.ManufacturingSkillRepository.FindById(this.manufacturingSkill.SkillCode)
                .Returns(this.manufacturingSkill);
            this.result = this.Sut.Update(this.manufacturingSkill.SkillCode, this.resource);
        }

        [Test]
        public void ShouldGetManufacturingSkill()
        {
            this.ManufacturingSkillRepository.Received().FindById(this.manufacturingSkill.SkillCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ManufacturingSkill>>();
            var dataResult = ((SuccessResult<ManufacturingSkill>)this.result).Data;
            dataResult.SkillCode.Should().Be(this.manufacturingSkill.SkillCode);
            dataResult.Description.Should().Be(NewDesc);
            dataResult.HourlyRate.Should().Be(NewHourlyRate);
            dataResult.DateInvalid.Should().Be(DateTime.Today);
        }
    }
}
