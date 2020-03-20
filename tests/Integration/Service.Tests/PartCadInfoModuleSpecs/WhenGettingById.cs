namespace Linn.Production.Service.Tests.PartCadInfoModuleSpecs
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
            var partCadInfo = new PartCadInfo { MsId = 123 };

            this.PartCadInfoService.GetById(123).Returns(new SuccessResult<PartCadInfo>(partCadInfo));

            this.Response = this.Browser.Get(
                "/production/maintenance/part-cad-info/123",
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
            this.PartCadInfoService.Received().GetById(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartCadInfoResource>();
            resource.MsId.Should().Be(123);
        }
    }
}
