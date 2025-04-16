namespace Linn.Production.Service.Tests.ManufacturingSkillsModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingManufacturingSkill : ContextBase
    {
        private ManufacturingSkillResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ManufacturingSkillResource { SkillCode = "MYTEST", Description = "Desc1", HourlyRate = 150 };
            var skill = new ManufacturingSkill { SkillCode = "MYTEST", Description = "Desc1", HourlyRate = 150 };
            this.ManufacturingSkillService.Update("MYTEST", Arg.Any<ManufacturingSkillResource>())
                .Returns(new SuccessResult<ManufacturingSkill>(skill));

            this.Response = this.Browser.Put(
                "/production/resources/manufacturing-skills/MYTEST",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ManufacturingSkillService.Received()
                .Update("MYTEST", Arg.Is<ManufacturingSkillResource>(r => r.SkillCode == this.requestResource.SkillCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingSkillResource>();
            resource.SkillCode.Should().Be("MYTEST");
            resource.Description.Should().Be("Desc1");
            resource.HourlyRate.Should().Be(150);
        }
    }
}
