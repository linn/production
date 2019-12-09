namespace Linn.Production.Service.Tests.LabelModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingApplicationState : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Get(
                "/production/maintenance/labels/reprint-reasons/application-state",
                with =>
                    {
                        with.Header("Accept", "application/vnd.linn.application-state+json;version=1");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldNotCallService()
        {
            this.LabelReprintFacadeService.DidNotReceive().GetById(Arg.Any<int>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnEmptyResource()
        {
            var resource = this.Response.Body.DeserializeJson<LabelReprintResource>();
            resource.LabelReprintId.Should().Be(0);
        }

        [Test]
        public void ShouldContainLink()
        {
            var resource = this.Response.Body.DeserializeJson<LabelReprintResource>();
            resource.Links.Should().HaveCount(1);
            resource.Links.Should().Contain(l => l.Rel == "create" && l.Href == "/production/maintenance/labels/reprint-reasons");
        }
    }
}