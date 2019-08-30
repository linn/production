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

    public class WhenAddingManufacturingSkill : ContextBase
    {
        private ManufacturingSkillResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ManufacturingSkillResource() { SkillCode = "ADDTEST", Description = "Desc12", HourlyRate = 151 };
            var newSkill = new ManufacturingSkill("ADDTEST", "Desc12", 151);

            this.ManufacturingSkillService.Add(Arg.Any<ManufacturingSkillResource>())
                .Returns(new CreatedResult<ManufacturingSkill>(newSkill));

            this.Response = this.Browser.Post(
                "/production/resources/manufacturing-skills",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ManufacturingSkillService.Received()
                .Add(Arg.Is<ManufacturingSkillResource>(r => r.SkillCode == this.requestResource.SkillCode));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingSkillResource>();
            resource.SkillCode.Should().Be("ADDTEST");
            resource.Description.Should().Be("Desc12");
            resource.HourlyRate.Should().Be(151);
        }
    }
}
