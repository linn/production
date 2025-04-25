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

    public class WhenGettingManufacturingSkill : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var skill = new ManufacturingSkill("TESTCODE", "desc", 155, null);
            this.ManufacturingSkillFacadeService.GetById("TESTCODE")
                .Returns(new SuccessResult<ManufacturingSkill>(skill));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-skills/TESTCODE",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ManufacturingSkillFacadeService.Received().GetById("TESTCODE");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingSkillResource>();
            resource.SkillCode.Should().Be("TESTCODE");
            resource.Description.Should().Be("desc");
            resource.HourlyRate.Should().Be(155);
        }
    }
}
