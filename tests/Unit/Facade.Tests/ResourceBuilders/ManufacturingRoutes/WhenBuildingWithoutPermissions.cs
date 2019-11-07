namespace Linn.Production.Facade.Tests.ResourceBuilders.ManufacturingRoutes
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class WhenBuildingWithoutPermissions : ContextBase
    {
        private ManufacturingRouteResource result;

        private ManufacturingRoute manufacturingRoute;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingRoute = new ManufacturingRoute("112", "desc", "note");

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.ManufacturingRouteUpdate, Arg.Any<List<string>>())
                .Returns(false);

            this.result = this.Sut.Build(new ResponseModel<ManufacturingRoute>(this.manufacturingRoute, new List<string>()));
        }

        [Test]
        public void ShouldCreateResource()
        {
            this.result.RouteCode.Should().Be("112");
            this.result.Links.Should().Contain(l => l.Rel == "self");
        }

        [Test]
        public void ShouldNotHaveRestrictedLinkRels()
        {
            this.result.Links.Length.Should().Be(1);
            this.result.Links.Should().NotContain(l => l.Rel == "edit");
        }
    }
}
