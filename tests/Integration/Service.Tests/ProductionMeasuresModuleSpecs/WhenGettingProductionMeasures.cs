namespace Linn.Production.Service.Tests.ProductionMeasuresModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingProductionMeasures : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var productionMeasures = new ProductionMeasures
            {
                Cit = new Cit { Code = "V", Name = "PCB", BuildGroup = "EP", SortOrder = 2 },
                Ones = 5,
                Fives = 5,
                StockValue = 200
            };

            var productionMeasures2 = new ProductionMeasures
            {
                Cit = new Cit { Code = "F", Name = "Final Assembly", BuildGroup = "PP", SortOrder = 1 },
                Ones = 1,
                Twos = 2,
                Threes = 3,
                Fours = 4,
                Fives = 5,
                StockValue = 100
            };

            this.ProductionMeasuresReportFacade.GetProductionMeasuresForCits()
                .Returns(new SuccessResult<IEnumerable<ProductionMeasures>>(new List<ProductionMeasures> { productionMeasures, productionMeasures2 }));

            this.Response = this.Browser.Get(
                "/production/reports/measures/cits",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ProductionMeasuresReportFacade.Received().GetProductionMeasuresForCits();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ProductionMeasuresResource>>().ToList();
            resources.Should().HaveCount(2);
            resources[0].CitCode.Should().Be("F");
            resources[1].CitCode.Should().Be("V");
        }
    }
}
