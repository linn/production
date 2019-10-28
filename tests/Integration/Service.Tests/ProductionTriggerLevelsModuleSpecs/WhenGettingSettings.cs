namespace Linn.Production.Service.Tests.ProductionTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSettings : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var responseModel = new ResponseModel<PtlSettings>(new PtlSettings { DaysToLookAhead = 2 }, new List<string>());
            this.PtlSettingsFacadeService.Get(Arg.Any<List<string>>())
                .Returns(new SuccessResult<ResponseModel<PtlSettings>>(responseModel));

            this.Response = this.Browser.Get(
                "/production/maintenance/production-trigger-levels-settings",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.PtlSettingsFacadeService.Received().Get(Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PtlSettingsResource>();
            resource.DaysToLookAhead.Should().Be(2);
        }
    }
}