namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingComponentCountForPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
          
            this.CountComponentsService.CountComponents("PART")
                .Returns(new SuccessResult<ComponentCount>(new ComponentCount { SmtComponents = 1, PcbComponents = 1 }));

            this.Response = this.Browser.Get(
                "/production/quality/ate-tests/count-components/PART",
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
            this.CountComponentsService.Received().CountComponents(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<ComponentCountResource>();
            resources.PcbComponents.Should().Be(1);
            resources.PcbComponents.Should().Be(1);
        }
    }
}