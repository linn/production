namespace Linn.Production.Facade.Tests.WhoBuiltWhatFacadeSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WhoBuiltWhatReportFacadeService Sut { get; set; }

        protected IWhoBuiltWhatReport WhoBuiltWhatReport { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WhoBuiltWhatReport = Substitute.For<IWhoBuiltWhatReport>();
            this.Sut = new WhoBuiltWhatReportFacadeService(this.WhoBuiltWhatReport);
        }
    }
}