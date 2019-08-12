namespace Linn.Production.Service.Tests.BoardFailTypesModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingBoardFailTypes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new BoardFailType { Type = 1};
            var b = new BoardFailType { Type = 2 };
            this.FacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<BoardFailType>>(new List<BoardFailType> { a, b }));

            this.Response = this.Browser.Get(
                "/production/resources/board-fail-types",
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
            this.FacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<BoardFailTypeResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.FailType == 1);
            resources.Should().Contain(a => a.FailType == 2);
        }
    }
}