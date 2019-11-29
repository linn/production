//namespace Linn.Production.Facade.Tests.ProductionTriggerLevelFacadeServiceSpecs
//{
//    using FluentAssertions;
//    using Linn.Common.Facade;
//    using Linn.Production.Domain.LinnApps;
//    using Linn.Production.Domain.LinnApps.Triggers;
//    using Linn.Production.Resources;
//    using NSubstitute;
//    using NUnit.Framework;
//    using System.Collections.Generic;

//    public class WhenUpdating : ContextBase
//    {
//        private ProductionTriggerLevelResource resource;

//        private ProductionTriggerLevel triggerLevel;

//        private IResult<ResponseModel<ProductionTriggerLevel>> result;

//        [SetUp]
//        public void SetUp()
//        {
//            this.triggerLevel = new ProductionTriggerLevel();
//            this.resource = new ProductionTriggerLevelResource
//            {
//                PartNumber = "part1",
//                Description = "desc",
//                CitCode = "cit1",
//                VariableTriggerLevel = 1,
//                OverrideTriggerLevel = 2,
//                KanbanSize = 1,
//                MaximumKanbans = 2,
//                RouteCode = "pcas1",
//                WorkStation = "station1",
//                FaZoneType = "flex",
//                EngineerId = 33000,
//                Temporary = 'Y',
//                Story = ""
//            };

//            var privileges = new List<string> { AuthorisedAction.ProductionTriggerLevelUpdate };
//            this.result = this.Sut.Update(this.resource.PartNumber, this.resource, privileges);
//        }

//        [Test]
//        public void ShouldGetRecord()
//        {
//            this.TriggerLevelRepository.Received().();
//        }

//        [Test]
//        public void ShouldReturnSuccess()
//        {
//            this.result.Should().BeOfType<SuccessResult<PtlSettings>>();
//            var dataResult = ((SuccessResult<PtlSettings>)this.result).Data;
//            dataResult.BuildToMonthEndFromDays.Should().Be(1);
//            dataResult.DaysToLookAhead.Should().Be(2);
//            dataResult.FinalAssemblyDaysToLookAhead.Should().Be(3);
//            dataResult.PriorityCutOffDays.Should().Be(4);
//            dataResult.PriorityStrategy.Should().Be(5);
//            dataResult.SubAssemblyDaysToLookAhead.Should().Be(6);
//        }
//    }
//}
