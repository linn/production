namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRequestingTriggerRunButNotAuthorised : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.StartTriggerRun, Arg.Any<List<string>>())
                .Returns(false);
            this.Response = this.Browser.Post(
                "/production/maintenance/production-trigger-levels-settings/start-trigger-run",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}