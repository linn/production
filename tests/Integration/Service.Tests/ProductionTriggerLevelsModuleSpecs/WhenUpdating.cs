namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class WhenUpdating : ContextBase
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

            this.ProductionTriggerLevelService
                .Update(
                    "part1",
                    Arg.Any<ProductionTriggerLevelResource>(),
                    Arg.Any<List<string>>()).Returns(
                    new SuccessResult<ResponseModel<ProductionTriggerLevel>>(responseModel));

            this.Response = this.Browser.Put(
                "/production/maintenance/production-trigger-levels/part1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(this.resource);
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
            this.ProductionTriggerLevelService.Received().Update("part1", Arg.Is<ProductionTriggerLevelResource>(r => r.PartNumber == "part1"), Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resourceResult = this.Response.Body.DeserializeJson<ProductionTriggerLevelResource>();
            resourceResult.PartNumber.Should().Be("part1");
            resourceResult.Description.Should().Be("desc");
            resourceResult.CitCode.Should().Be("cit1");
            resourceResult.VariableTriggerLevel.Should().Be(1);
            resourceResult.OverrideTriggerLevel.Should().Be(2);
            resourceResult.KanbanSize.Should().Be(1);
            resourceResult.MaximumKanbans.Should().Be(2);
            resourceResult.RouteCode.Should().Be("pcas1");
            resourceResult.WorkStationName.Should().Be("station1");
            resourceResult.FaZoneType.Should().Be("flex");
            resourceResult.EngineerId.Should().Be(33000);
            resourceResult.Temporary.Should().Be("Y");
            resourceResult.Story.Should().Be("");
        }
    }
}
