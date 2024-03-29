﻿namespace Linn.Production.Service.Tests.ManufacturingTimingsReportModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingBuildsDetailExport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" }).ConvertToCsvList();
            this.Service.GetManufacturingTimingsExport(
                    DateTime.UnixEpoch,
                    DateTime.UnixEpoch,
                    Arg.Any<string>())
                .Returns(
                    new SuccessResult<IEnumerable<IEnumerable<string>>>(results)
                    {
                        Data = new List<List<string>> { new List<string> { "string" } }
                    });

            this.Response = this.Browser.Get(
                "/production/reports/manufacturing-timings/export",
                with =>
                {
                    with.Header("Accept", "text/csv");
                    with.Header("Accept", "application/json");
                    with.Query("startDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("endDate", DateTime.UnixEpoch.ToString("d"));
                    with.Query("citCode", "MW");
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
            this.Service.Received().GetManufacturingTimingsExport(
                DateTime.UnixEpoch,
                DateTime.UnixEpoch,
                Arg.Any<string>());
        }
    }
}
