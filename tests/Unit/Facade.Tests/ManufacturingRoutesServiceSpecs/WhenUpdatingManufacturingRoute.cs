namespace Linn.Production.Facade.Tests.ManufacturingRoutesServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingManufacturingRoute: ContextBase
    {

        private ManufacturingRouteResource resource;

        private ManufacturingOperation manufacturingOperation;

        private ManufacturingOperationResource opResource;

        private ManufacturingRoute manufacturingRoute;

        private IResult<ManufacturingRoute> result;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingRoute = new ManufacturingRoute("code red", "wood chuck chuck", "noted");
            this.manufacturingOperation = new ManufacturingOperation(
                "code red",
                77,
                15,
                "descrip of op",
                "codeOfOperation",
                "res Code",
                27,
                54,
                5,
                "cit code test");
            this.manufacturingRoute.Operations = new List<ManufacturingOperation>() { this.manufacturingOperation };

            this.opResource = new ManufacturingOperationResource
                                  {
                                      RouteCode = "code red",
                                      ManufacturingId = 77,
                                      OperationNumber = 15,
                                      Description = "descrip of op",
                                      SkillCode = "codeOfOperation",
                                      ResourceCode = "res Code",
                                      SetAndCleanTime = 27,
                                      CycleTime = 54,
                                      LabourPercentage = 5,
                                      CITCode = "cit code test"
                                  };
            this.resource = new ManufacturingRouteResource() { RouteCode = "code red", Description = "wood chuck chuck", Notes = "noted", Operations = new List<ManufacturingOperationResource>() { this.opResource } };

            this.ManufacturingRouteRepository
                .FindById(this.manufacturingRoute.RouteCode)
                .Returns(this.manufacturingRoute);

            this.ManufacturingOperationRepository
                .FindById(this.manufacturingOperation.ManufacturingId)
                .Returns(this.manufacturingOperation);

            this.result = this.Sut.Update(this.manufacturingRoute.RouteCode, this.resource);
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
            dataResult.Operations.Any(x => x.RouteCode == this.manufacturingOperation.RouteCode && x.OperationNumber == this.manufacturingOperation.OperationNumber).Should().BeTrue();
        }
    }
}
