namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ptl1 = new ProductionTriggerLevel { PartNumber = "pcas1", Description = "d1" };
            var ptl2 = new ProductionTriggerLevel { PartNumber = "pcas2", Description = "d2" };
            var requestResource = new ProductionTriggerLevelsSearchRequestResource
            {
                SearchTerm = "pcas",
                CitSearchTerm = "S",
                AutoSearchTerm = "0",
                OverrideSearchTerm = "0",
            };

            this.ProductionTriggerLevelService.Search(
                    Arg.Is<ProductionTriggerLevelsSearchRequestResource>(
                        x => x.CitSearchTerm == "S" && x.SearchTerm == "pcas" && x.AutoSearchTerm == "0" && x.OverrideSearchTerm == "0"),
                    Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>>(
                    new ResponseModel<IEnumerable<ProductionTriggerLevel>>(
                        new List<ProductionTriggerLevel> { ptl1, ptl2 },
                        new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/production-trigger-levels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("SearchTerm", requestResource.SearchTerm);
                        with.Query("AutoSearchTerm", requestResource.AutoSearchTerm);
                        with.Query("OverrideSearchTerm", requestResource.OverrideSearchTerm);
                        with.Query("CitSearchTerm", requestResource.CitSearchTerm);
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
            this.ProductionTriggerLevelService.Received().Search(
                Arg.Is<ProductionTriggerLevelsSearchRequestResource>(x => x.CitSearchTerm == "S"
                                                                          && x.SearchTerm == "pcas"
                                                                          && x.AutoSearchTerm == "0"
                                                                          && x.OverrideSearchTerm == "0"),
                Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ProductionTriggerLevel>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.PartNumber == "pcas1");
            resources.Should().Contain(a => a.PartNumber == "pcas2");
        }
    }
}
