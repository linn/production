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

    public class WhenUpdating : ContextBase
    {
        private MechPartSourceResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new MechPartSourceResource { MsId = 123, Description = "DESC" };

            var mechPartSource = new MechPartSource { MsId = 123, Description = "DESC" };

            this.MechPartSourceService.Update(123, Arg.Any<MechPartSourceResource>())
                .Returns(new SuccessResult<MechPartSource>(mechPartSource));

            this.Response = this.Browser.Put(
                "/production/maintenance/mech-part-source/123",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.MechPartSourceService.Received().Update(123, Arg.Any<MechPartSourceResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSourceResource>();
            resource.MsId.Should().Be(123);
            resource.Description.Should().Be("DESC");
        }
    }
}