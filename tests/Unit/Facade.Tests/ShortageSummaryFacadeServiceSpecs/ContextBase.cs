namespace Linn.Production.Facade.Tests.ShortageSummaryFacadeServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ShortageSummaryFacadeService Sut { get; set; }

        protected IShortageSummaryReportService ShortageSummaryReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ShortageSummaryReportService = Substitute.For<IShortageSummaryReportService>();
            this.Sut = new ShortageSummaryFacadeService(this.ShortageSummaryReportService);
        }
    }
}
