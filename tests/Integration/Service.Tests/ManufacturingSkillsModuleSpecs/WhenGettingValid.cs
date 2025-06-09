namespace Linn.Production.Service.Tests.ManufacturingSkillsModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingManufacturingSkills : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new ManufacturingSkill("a", "desc", 15);
            var b = new ManufacturingSkill("b", "desc", 17);
            this.ManufacturingSkillFacadeService.FilterBy(Arg.Any<IncludeInvalidRequestResource>())
                .Returns(new SuccessResult<IEnumerable<ManufacturingSkill>>(new List<ManufacturingSkill> { a, b }));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-skills",
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
            this.ManufacturingSkillFacadeService.Received().FilterBy(Arg.Any<IncludeInvalidRequestResource>());
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
