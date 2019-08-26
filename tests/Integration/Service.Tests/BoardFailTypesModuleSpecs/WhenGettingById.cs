namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingBoardFailType : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new BoardFailType {Type = 1, Description = "desc"};
            this.FacadeService.GetById(1)
                .Returns(new SuccessResult<BoardFailType>(a));

            this.Response = this.Browser.Get(
                "/production/resources/board-fail-types/1",
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
            this.FacadeService.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BoardFailTypeResource>();
            resource.FailType.Should().Be(1);
            resource.Description.Should().Be("desc");
        }
    }
}
