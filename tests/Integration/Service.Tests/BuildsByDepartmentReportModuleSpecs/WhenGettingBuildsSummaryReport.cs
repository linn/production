﻿namespace Linn.Production.Service.Tests.BuildsByDepartmentReportModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildsSummaryReport : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel { ReportTitle = new NameModel("t") };
            this.BuildsByDepartmentReportFacadeService
                .GetBuildsSummaryReport(DateTime.UnixEpoch, DateTime.UnixEpoch, false)
                .Returns(
                    new SuccessResult<ResultsModel>(results)
                        {
                            Data = new ResultsModel { ReportTitle = new NameModel("t")}
                        });

            this.Response = this.Browser.Get(
                "/production/reports/builds-summary",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("fromDate", DateTime.UnixEpoch.ToString("d"));
                        with.Query("toDate", DateTime.UnixEpoch.ToString("d"));
                        with.Query("monthly", "false");
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
            this.BuildsByDepartmentReportFacadeService.Received().GetBuildsSummaryReport(
                DateTime.UnixEpoch,
                DateTime.UnixEpoch,
                false);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("t");
        }
    }
}