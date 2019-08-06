namespace Linn.Production.Service.Tests.BuildsByDepartmentReportModuleSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildsDetailExport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" }).ConvertToCsvList();
            this.BuildsByDepartmentReportFacadeService.GetBuildsDetailExport(
                    DateTime.UnixEpoch,
                    DateTime.UnixEpoch,
                    "001",
                    "Value",
                    false)
                .Returns(
                    new SuccessResult<IEnumerable<IEnumerable<string>>>(results)
                    {
                        Data = new List<List<string>> { new List<string> { "string"} }
                    });

            this.Response = this.Browser.Get(
                "/production/reports/builds-detail/export",
                with =>
                {
                    with.Header("Accept", "text/csv");
                    with.Header("Accept", "application/json");
                    with.Query("fromDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("toDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("monthly", "false");
                    with.Query("quantityOrValue", "Value");
                    with.Query("department", "001");
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
            this.BuildsByDepartmentReportFacadeService.Received().GetBuildsDetailExport(
                DateTime.UnixEpoch,
                DateTime.UnixEpoch,
                "001",
                "Value",
                false);
        }
    }
}
