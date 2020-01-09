namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
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

    public class WhenRemoving : ContextBase
    {
        private ProductionTriggerLevelResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ProductionTriggerLevelResource
                                {
                                    PartNumber = "part1",
                                    Description = "desc",
                                    CitCode = "cit1",
                                    VariableTriggerLevel = 1,
                                    OverrideTriggerLevel = 2,
                                    KanbanSize = 1,
                                    MaximumKanbans = 2,
                                    RouteCode = "pcas1",
                                    WorkStationName = "station1",
                                    FaZoneType = "flex",
                                    EngineerId = 33000,
                                    Temporary = "Y",
                                    Story = string.Empty
                                };

            var triggerLevel = new ProductionTriggerLevel
                                   {
                                       PartNumber = "part1",
                                       Description = "desc",
                                       CitCode = "cit1",
                                       VariableTriggerLevel = 1,
                                       OverrideTriggerLevel = 2,
                                       KanbanSize = 1,
                                       MaximumKanbans = 2,
                                       RouteCode = "pcas1",
                                       WorkStationName = "station1",
                                       FaZoneType = "flex",
                                       EngineerId = 33000,
                                       Temporary = "Y",
                                       Story = string.Empty
                                   };

            var responseModel = new ResponseModel<ProductionTriggerLevel>(
                triggerLevel,
                new List<string>());

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, Arg.Any<List<string>>())
                .Returns(true);

            this.ProductionTriggerLevelService.Remove(Arg.Any<ProductionTriggerLevelResource>(), Arg.Any<List<string>>())
                .Returns(new SuccessResult<ResponseModel<ProductionTriggerLevel>>(responseModel));


            this.Response = this.Browser.Delete(
                "/production/maintenance/production-trigger-levels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(resource);
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
            this.ProductionTriggerLevelService.Received().Remove(Arg.Any<ProductionTriggerLevelResource>(), Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProductionTriggerLevelResource>();
            resource.PartNumber.Should().Be("part1");
        }
    }
}
