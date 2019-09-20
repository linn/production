namespace Linn.Production.Facade.Tests.ResourceBuilders.PtlSettingsSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBuildingWithoutPermissions : ContextBase
    {
        private PtlSettingsResource result;

        private PtlSettings ptlSettings;

        [SetUp]
        public void SetUp()
        {
            this.ptlSettings = new PtlSettings { BuildToMonthEndFromDays = 1 };

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.PtlSettingsUpdate, Arg.Any<List<string>>())
                .Returns(false);
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.StartTriggerRun, Arg.Any<List<string>>())
                .Returns(false);

            this.result = this.Sut.Build(new ResponseModel<PtlSettings>(this.ptlSettings, new List<string>()));
        }

        [Test]
        public void ShouldCreateResource()
        {
            this.result.BuildToMonthEndFromDays.Should().Be(1);
            this.result.Links.Should().Contain(l => l.Rel == "self");
        }

        [Test]
        public void ShouldNotHaveRestrictedLinkRels()
        {
            this.result.Links.Should().NotContain(l => l.Rel == "edit");
            this.result.Links.Should().NotContain(l => l.Rel == "start-trigger-run");
        }
    }
}
