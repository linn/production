namespace Linn.Production.Facade.Tests.ManufacturingRoutesServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class WhenUpdatingManufacturingRoute : ContextBase
    {
        private readonly string routeCode = "code red";

        private ManufacturingRouteResource resource;

        private IEnumerable<ManufacturingOperation> manufacturingOperations;

        private IEnumerable<ManufacturingOperationResource> opResources;

        private ManufacturingRoute manufacturingRoute;

        private IResult<ManufacturingRoute> result;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingRoute = new ManufacturingRoute("code red", "wood chuck chuck", "noted");
            this.manufacturingOperations = new List<ManufacturingOperation>
                                               {
                                                   new ManufacturingOperation(
                                                       this.routeCode,
                                                       77,
                                                       15,
                                                       "descrip of op",
                                                       "codeOfOperation",
                                                       "res Code",
                                                       27,
                                                       54,
                                                       5,
                                                       "cit code test"),
                                                   new ManufacturingOperation(
                                                       this.routeCode,
                                                       78,
                                                       16,
                                                       "descripop",
                                                       "sklcode",
                                                       "rescd",
                                                       28,
                                                       55,
                                                       6,
                                                       "code test")
                                               };
            this.manufacturingRoute.Operations = this.manufacturingOperations;

            this.opResources = new List<ManufacturingOperationResource> {
                new ManufacturingOperationResource {
                    RouteCode = this.routeCode,
                    ManufacturingId = 77,
                    OperationNumber = 15,
                    Description = "descrip of op",
                    SkillCode = "codeOfOperation",
                    ResourceCode = "res Code",
                    SetAndCleanTime = 27,
                    CycleTime = 54,
                    LabourPercentage = 5,
                    CITCode = "cit code test"
                },
               new ManufacturingOperationResource {
                    RouteCode = this.routeCode,
                    ManufacturingId = 78,
                    OperationNumber = 16,
                    Description = "descripop",
                    SkillCode = "sklcode",
                    ResourceCode = "rescd",
                    SetAndCleanTime = 28,
                    CycleTime = 55,
                    LabourPercentage = 6,
                    CITCode = "code test"
                }
             };
            this.resource = new ManufacturingRouteResource
            {
                RouteCode = this.routeCode,
                Description = "wood chuck chuck",
                Notes = "noted",
                Operations = this.opResources
            };

            this.ManufacturingRouteRepository.FindById(this.routeCode)
                .Returns(this.manufacturingRoute);

            this.ManufacturingOperationRepository.FindById(77)
                .Returns(this.manufacturingOperations.First(x => x.ManufacturingId == 77));

            this.result = this.Sut.Update(this.routeCode, this.resource);
        }

        [Test]
        public void ShouldGetManufacturingRoute()
        {
            this.ManufacturingRouteRepository.Received().FindById(this.manufacturingRoute.RouteCode);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ManufacturingRoute>>();
            var dataResult = ((SuccessResult<ManufacturingRoute>)this.result).Data;
            dataResult.RouteCode.Should().Be(this.manufacturingRoute.RouteCode);
            dataResult.Description.Should().Be(this.manufacturingRoute.Description);
            dataResult.Notes.Should().Be(this.manufacturingRoute.Notes);
            dataResult.Operations.Any(
                x => x.RouteCode == this.routeCode
                     && x.ManufacturingId == 77).Should().BeTrue();
            dataResult.Operations.Any(
                x => x.RouteCode == this.routeCode
                     && x.ManufacturingId == 71).Should().BeFalse();
            dataResult.Operations.Count().Should().Be(2);
        }
    }
}
