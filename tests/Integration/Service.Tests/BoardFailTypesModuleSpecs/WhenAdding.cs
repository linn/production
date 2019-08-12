namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using System.Collections;
    using System.Collections.Generic;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingBoardFailType : ContextBase
    {
        private BoardFailTypeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new BoardFailTypeResource { FailType = 1, Description = "Desc" };
            var newSkill = new BoardFailType { Type = 1, Description = "Desc" };

            this.FacadeService.Add(Arg.Any<BoardFailTypeResource>())
                .Returns(new CreatedResult<BoardFailType>(newSkill));

            this.Response = this.Browser.Post(
                "/production/resources/board-fail-types",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FacadeService.Received()
                .Add(Arg.Is<BoardFailTypeResource>(r => r.FailType == this.requestResource.FailType));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BoardFailTypeResource>();
            resource.FailType.Should().Be(1);
            resource.Description.Should().Be("Desc");
        }
    }
}
