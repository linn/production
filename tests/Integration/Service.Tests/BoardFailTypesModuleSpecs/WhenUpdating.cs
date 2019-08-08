namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingBoardFailType : ContextBase
    {
        private BoardFailTypeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new BoardFailTypeResource { FailType = 1, Description = "New Desc" };
            var a = new BoardFailType { Type = 1, Description = "New Desc" };
            this.FacadeService.Update(1, Arg.Any<BoardFailTypeResource>())
                .Returns(new SuccessResult<BoardFailType>(a));

            this.Response = this.Browser.Put(
                "/production/resources/board-fail-types/1",
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
            this.FacadeService.Received()
                .Update(1, Arg.Is<BoardFailTypeResource>(r => r.FailType == this.requestResource.FailType));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BoardFailTypeResource>();
            resource.FailType.Should().Be(1);
            resource.Description.Should().Be("New Desc");
        }
    }
}
