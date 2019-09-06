namespace Linn.Production.Service.Tests.ProductionTriggersModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingProductionTriggersReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var report = new ProductionTriggersReport()
            {
                Cit = new Cit { Code = "A", Name = "Army of the Undead" },
                PtlMaster = new PtlMaster { LastFullRunJobref = "CJCAIH", LastFullRunDateTime = new DateTime(2019, 1, 1) },
                Triggers = new List<ProductionTrigger>
                {
                    new ProductionTrigger { PartNumber = "A", Description = "A product", Priority = "1" },
                    new ProductionTrigger { PartNumber = "B", Description = "B nice", Priority = "2" },
                    new ProductionTrigger { PartNumber = "C", Description = "C", Priority = "3" }
                }
            };

            this.ProductionTriggersFacadeService.GetProductionTriggerReport("CJCAIH", "A")
                .Returns(new SuccessResult<ProductionTriggersReport>(report));

            this.Response = this.Browser.Get(
                "/production/reports/triggers",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.Query("jobref", "CJCAIH");
                    with.Query("citCode", "A");
                    with.Query("reportType", "Full");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void SholdCallService()
        {
            this.ProductionTriggersFacadeService.Received()
                .GetProductionTriggerReport("CJCAIH", "A");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var results = this.Response.Body.DeserializeJson<ProductionTriggerReportResultsResource>();
            results.ReportResults.CitCode.Should().Be("A");
        }
    }
}