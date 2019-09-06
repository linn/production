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
    using NSubstitute.Core.Arguments;
    using NUnit.Framework;

    public class WhenGettingTriggersWithNoCit : ContextBase
    {
        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            this.PtlMasterRepository.GetMasterRecord().Returns(new PtlMaster { LastFullRunJobref = "AAAAAA" });

            var cits = new List<Cit>
            {
                new Cit() { Code = "S", Name = "A Team", BuildGroup = "PP", SortOrder = 1 }
            };
            this.CitRepository.FilterBy(Arg.Any<Expression<Func<Cit, bool>>>()).Returns(cits.AsQueryable());
            this.result = this.Sut.GetProductionTriggerReport("CJCAIH", string.Empty);
        }

        [Test]
        public void ShouldReturnSuccessRequest()
        {
            this.result.Should().BeOfType<SuccessResult<ProductionTriggersReport>>();
        }

        [Test]
        public void ShouldHaveGotAnyCitFromTheRepo()
        {
            this.CitRepository.Received().FilterBy(Arg.Any<Expression<Func<Cit, bool>>>());
        }
    }
}