namespace Linn.Production.Facade.Tests.ManufacturingRoutesServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingManufacturingRoute : ContextBase
    {
        private ManufacturingRouteResource resource;

        private ManufacturingRoute manufacturingRoute;

        private IResult<ManufacturingRoute> result;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingRoute = new ManufacturingRoute("code red", "wood chuck chuck", "noted");

            this.resource = new ManufacturingRouteResource
                                {
                                    RouteCode = "code red", Description = "wood chuck chuck", Notes = "noted"
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddManufacturingRoute()
        {
            this.ManufacturingRouteRepository.Received().Add(Arg.Any<ManufacturingRoute>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<ManufacturingRoute>>();
            var dataResult = ((CreatedResult<ManufacturingRoute>)this.result).Data;
            dataResult.RouteCode.Should().Be(this.manufacturingRoute.RouteCode);
            dataResult.Description.Should().Be(this.manufacturingRoute.Description);
            dataResult.Notes.Should().Be(this.manufacturingRoute.Notes);
        }
    }
}
