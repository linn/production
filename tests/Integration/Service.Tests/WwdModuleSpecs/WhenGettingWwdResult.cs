namespace Linn.Production.Service.Tests.WwdModuleSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingWwdResult : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var wwdResults = new WwdResult()
            {
                PartNumber = "KYLEGUARD",
                WwdJobId = 1,
                PtlJobref = "CJCAIH",
                Qty = 5,
                WorkStationCode = "KGUARD",
                WwdDetails = new List<WwdDetail>
                {
                    new WwdDetail {PartNumber = "KYLE", Description = "KYLE", WwdJobId = 1, PtlJobref = "CJCAIH", QtyAtLocation = 1, QtyKitted = 5, StoragePlace = "K-ZONE"},
                    new WwdDetail {PartNumber = "GUARD", Description = "GUARD", WwdJobId = 1, PtlJobref = "CJCAIH", QtyAtLocation = 0, QtyKitted = 5, StoragePlace = "P100"}
                }
            };

            this.WwdResultFacadeService.GenerateWwdResultForTrigger("KYLEGUARD",5,"CJCAIH")
                .Returns(new SuccessResult<WwdResult>(wwdResults));

            this.Response = this.Browser.Get(
                "/production/reports/wwd",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("ptlJobref", "CJCAIH");
                    with.Query("qty", "5");
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
            this.WwdResultFacadeService.Received()
                .GenerateWwdResultForTrigger("KYLEGUARD", 5, "CJCAIH");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var results = this.Response.Body.DeserializeJson<WwdResultReportResource>();
            results.ReportResults.PartNumber.Should().Be("KYLEGUARD");
        }
    }
}
