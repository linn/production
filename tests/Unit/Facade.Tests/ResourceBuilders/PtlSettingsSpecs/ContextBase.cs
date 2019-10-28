namespace Linn.Production.Facade.Tests.ResourceBuilders.PtlSettingsSpecs
{
    using Linn.Common.Authorisation;
    using Linn.Production.Facade.ResourceBuilders;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PtlSettingsResourceBuilder Sut { get; set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            this.Sut = new PtlSettingsResourceBuilder(this.AuthorisationService);
        }
    }
}
