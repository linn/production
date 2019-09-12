namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggers : ContextBase
    {
        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            var cit = new Cit { Code = "A", Name = "Army of the Undead" };
            this.CitRepository.FindById(Arg.Any<string>()).Returns(cit);

            var master = new PtlMaster {LastFullRunJobref = "CJCAIH", LastFullRunDateTime = new DateTime(2019, 1, 1)};
            this.PtlMasterRepository.GetMasterRecord().Returns(master);

            var triggers = new List<ProductionTrigger>
            {
                new ProductionTrigger { PartNumber = "A", Description = "A product", Priority = "1" },
                new ProductionTrigger { PartNumber = "B", Description = "B nice", Priority = "2" },
                new ProductionTrigger { PartNumber = "C", Description = "C", Priority = "3" }
            };
            this.ProductionTriggerQueryRepository.FilterBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(triggers.AsQueryable());

            this.result = this.Sut.GetProductionTriggerReport("CJCAIH", "A");
        }

        [Test]
        public void ShouldReturnSuccessRequest()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggersReport>>();
            var triggers = this.result.As<SuccessResult<ProductionTriggersReport>>().Data;
        }

        [Test]
        public void ShouldReturnTriggerReport()
        {
            var report = this.result.As<SuccessResult<ProductionTriggersReport>>().Data;
            report.PtlMaster.LastFullRunJobref.Should().Be("CJCAIH");
            report.Cit.Code.Should().Be("A");

            var triggers = report.Triggers;
            triggers.Count().Should().Be(3);
            triggers.First().PartNumber.Should().Be("A");
        }
    }
}