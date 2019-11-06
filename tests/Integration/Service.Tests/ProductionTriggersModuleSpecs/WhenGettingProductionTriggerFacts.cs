namespace Linn.Production.Service.Tests.ProductionTriggersModuleSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingProductionTriggerFacts : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var facts = new ProductionTriggerFacts(new ProductionTrigger { PartNumber = "KYLEGUARD", Description = "A product", Priority = "1" })
            {
                OutstandingWorksOrders = new List<WorksOrder>(),
                OutstandingSalesOrders = new List<ProductionBackOrder>(),
                WhereUsedAssemblies = new List<ProductionTriggerAssembly>()
            };

            this.ProductionTriggersFacadeService.GetProductionTriggerFacts("CJCAIH", "KYLEGUARD")
                .Returns(new SuccessResult<ProductionTriggerFacts>(facts));

            this.Response = this.Browser.Get(
                "/production/reports/triggers/facts",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("jobref", "CJCAIH");
                    with.Query("partNumber", "KYLEGUARD");
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
                .GetProductionTriggerFacts("CJCAIH", "KYLEGUARD");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var results = this.Response.Body.DeserializeJson<ProductionTriggerFactsResultsResource>();
            results.ReportResults.PartNumber.Should().Be("KYLEGUARD");
        }
    }
}