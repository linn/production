﻿namespace Linn.Production.Service.Tests.BuiltThisWeekModuleSpecs
{
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.BtwResultFacadeService.GenerateBtwResultForCit(
                    "S")
                .Returns(
                    new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel
                        {
                            ReportTitle =
                            new NameModel("title")
                        }
                    });

            this.Response = this.Browser.Get(
                "/production/reports/btw",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Accept", "application/json");
                    with.Query("citCode", "S");
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
            this.BtwResultFacadeService.Received().GenerateBtwResultForCit(
                "S");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}
