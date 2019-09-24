namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRequestingTriggerRunFails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.StartTriggerRun, Arg.Any<List<string>>())
                .Returns(true);
            this.TriggerRunDispatcher
                .When(d => d.StartTriggerRun(Arg.Any<string>()))
                .Do(e => throw new Exception("failed failed"));
            this.Response = this.Browser.Post(
                "/production/maintenance/production-trigger-levels-settings/start-trigger-run",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ErrorResource>();
            resource.Errors.First().Should().Be("failed failed");
        }
    }
}