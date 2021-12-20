namespace Linn.Production.Service.Tests.ManufacturingTimingsReportModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOperationsTimingsForABom : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new List<List<string>> { new List<string> { "string" } };
            this.Service.GetTimingsForAssembliesOnABom("BOM")
                .Returns(
                    new SuccessResult<IEnumerable<IEnumerable<string>>>(results)
                        {
                            Data = results
                        });

            this.Response = this.Browser.Get(
                "/production/reports/bom-labour-routes",
                                                 with =>
                                                     {
                                                         with.Header("Accept", "text/csv");
                                                         with.Header("Accept", "application/json");
                                                         with.Query("searchTerm", "BOM");
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
            this.Service.Received().GetTimingsForAssembliesOnABom("BOM");
        }
    }
}
