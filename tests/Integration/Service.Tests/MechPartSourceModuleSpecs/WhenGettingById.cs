namespace Linn.Production.Service.Tests.MechPartSourceModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var mechPartSource = new MechPartSource { MsId = 123 };

            this.MechPartSourceService.GetById(123).Returns(new SuccessResult<MechPartSource>(mechPartSource));

            this.Response = this.Browser.Get(
                "/production/maintenance/mech-part-source/123",
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
            this.MechPartSourceService.Received().GetById(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSourceResource>();
            resource.MsId.Should().Be(123);
        }
    }
}
