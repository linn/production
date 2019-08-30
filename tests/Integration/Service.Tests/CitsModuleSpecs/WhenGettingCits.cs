namespace Linn.Production.Service.Tests.CitModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Service.Tests.CitsModuleSpecs;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCits : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var cit1 = new Cit { Code = "a", Name = "A" };
            var cit2 = new Cit { Code = "b", Name = "B" };
            this.CitService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Cit>>(new List<Cit> { cit1, cit2 }));

            this.Response = this.Browser.Get(
                "/production/maintenance/cits",
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
            this.CitService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<Cit>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.Name == "A");
            resources.Should().Contain(a => a.Name == "B");
        }
    }
}