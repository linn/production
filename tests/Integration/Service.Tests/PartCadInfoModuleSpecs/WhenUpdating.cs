namespace Linn.Production.Service.Tests.PartCadInfoModuleSpecs
{
    using System.Collections.Generic;

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
        private PartCadInfoResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartCadInfoResource { MsId = 123, Description = "DESC" };

            var partCadInfo = new PartCadInfo { MsId = 123, Description = "DESC" };

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.PartCadInfoUpdate, Arg.Any<List<string>>())
                .Returns(true);

            this.PartCadInfoService.Update(123, Arg.Any<PartCadInfoResource>(), Arg.Any<List<string>>()).Returns(
                new SuccessResult<ResponseModel<PartCadInfo>>(
                    new ResponseModel<PartCadInfo>(partCadInfo, new List<string>())));

            this.Response = this.Browser.Put(
                "/production/maintenance/part-cad-info/123",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PartCadInfoService.Received().Update(
                123,
                Arg.Any<PartCadInfoResource>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartCadInfoResource>();
            resource.MsId.Should().Be(123);
            resource.Description.Should().Be("DESC");
        }
    }
}