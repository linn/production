namespace Linn.Production.Domain.Tests.DeliveryPerformanceReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected DeliveryPeformanceReportService Sut { get; set; }

        protected IQueryRepository<PtlStat> PtlStatRepository { get; set; }

        protected ILinnWeekService LinnWeekService { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PtlStatRepository = Substitute.For<IQueryRepository<PtlStat>>();
            this.LinnWeekService = Substitute.For<ILinnWeekService>();
            this.Sut = new DeliveryPeformanceReportService(this.PtlStatRepository, this.LinnWeekService);
        }
    }
}
