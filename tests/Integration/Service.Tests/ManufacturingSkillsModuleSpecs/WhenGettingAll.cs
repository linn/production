using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Service.Tests.ManufacturingSkillsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingManufacturingSkills : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new ManufacturingSkill { SkillCode = "a"};
            var b = new ManufacturingSkill { SkillCode = "b" };
            this.ManufacturingSkillService.GetAll()
                .Returns(new SuccessResult<IEnumerable<ManufacturingSkill>>(new List<ManufacturingSkill> { a, b }));

            this.Response = this.Browser.Get(
                "/production/manufacturing-skills",
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
            this.ManufacturingSkillService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ManufacturingSkillResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.SkillCode == "a");
            resources.Should().Contain(a => a.SkillCode == "b");
        }
    }
}