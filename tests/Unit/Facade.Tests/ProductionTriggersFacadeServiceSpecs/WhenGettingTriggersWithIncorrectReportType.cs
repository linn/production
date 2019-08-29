﻿namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NUnit.Framework;

    public class WhenGettingTriggersWithIncorrectReportType : ContextBase
    {

        private IResult<ProductionTriggersReport> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetProductionTriggerReport("CJCAIH", "S", "Bigly");
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ProductionTriggersReport>>();
            this.result.As<BadRequestResult<ProductionTriggersReport>>().Message.Should().Be("Invalid report type");
        }
    }
}