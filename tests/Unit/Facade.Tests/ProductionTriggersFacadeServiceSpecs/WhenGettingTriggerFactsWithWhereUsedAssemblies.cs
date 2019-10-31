namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingTriggerFactsWithWhereUsedAssemblies : ContextBase
    {
        private IResult<ProductionTriggerFacts> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTrigger
            {
                PartNumber = "SERIES K", Description = "A serious product", Priority = "1",
                ReqtForInternalCustomersBI = 1
            };

            this.ProductionTriggerQueryRepository.FindBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(trigger);

            var productionTriggerAssemblies = new List<ProductionTriggerAssembly>
            {
                new ProductionTriggerAssembly
                {
                    Jobref = "AAAAAA", AssemblyNumber = "SERIES K01", PartNumber = "SERIES K",
                    ReqtForInternalAndTriggerLevelBT = 1
                },
                new ProductionTriggerAssembly()
                    {Jobref = "AAAAAA", AssemblyNumber = "SERIES KIT KAT", PartNumber = "SERIES K"}
            };

            this.ProductionTriggerAssemblyQueryRepository
                .FilterBy(Arg.Any<Expression<Func<ProductionTriggerAssembly, bool>>>())
                .Returns(productionTriggerAssemblies.AsQueryable());

            this.result = this.Sut.GetProductionTriggerFacts("CJCAIH", "SERIES K");
        }

        [Test]
        public void ShouldReturnSuccessRequestWithTriggerAndWhereUsedAssemblies()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggerFacts>>();
            var facts = this.result.As<SuccessResult<ProductionTriggerFacts>>().Data;
            facts.Trigger.PartNumber.Should().Be("SERIES K");
            facts.WhereUsedAssemblies.Count().Should().Be(1);
        }

        [Test]
        public void ShouldNotWorksOrderRepository()
        {
            this.ProductionTriggerAssemblyQueryRepository.Received()
                .FilterBy(Arg.Any<Expression<Func<ProductionTriggerAssembly, bool>>>());
        }
    }
}
