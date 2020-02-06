namespace Linn.Production.Facade.Tests.BtwResultServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BtwResultFacadeService Sut { get; set; }

        protected IBuiltThisWeekReportService BuiltThisWeekReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BuiltThisWeekReportService = Substitute.For<IBuiltThisWeekReportService>();
            this.Sut = new BtwResultFacadeService(this.BuiltThisWeekReportService);
        }
    }
}
