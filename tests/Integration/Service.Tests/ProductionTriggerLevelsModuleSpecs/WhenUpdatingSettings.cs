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

    public class WhenUpdatingSettings : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new PtlSettingsResource { FinalAssemblyDaysToLookAhead = 5 };
            var responseModel = new ResponseModel<PtlSettings>(new PtlSettings { FinalAssemblyDaysToLookAhead = 5 }, new List<string>());
            this.PtlSettingsFacadeService.Update(
                    Arg.Is<PtlSettingsResource>(r => r.FinalAssemblyDaysToLookAhead == 5),
                    Arg.Any<List<string>>())
                .Returns(new SuccessResult<ResponseModel<PtlSettings>>(responseModel));

            this.Response = this.Browser.Put(
                "/production/maintenance/production-trigger-levels-settings",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(resource);
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
            this.PtlSettingsFacadeService.Received().Update(Arg.Is<PtlSettingsResource>(r => r.FinalAssemblyDaysToLookAhead == 5), Arg.Any<List<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PtlSettingsResource>();
            resource.FinalAssemblyDaysToLookAhead.Should().Be(5);
        }
    }
}