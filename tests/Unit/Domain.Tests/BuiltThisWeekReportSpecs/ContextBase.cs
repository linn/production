namespace Linn.Production.Domain.Tests.BuiltThisWeekReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BuiltThisWeekReportService Sut { get; set; }

        protected IQueryRepository<BuiltThisWeekStatistic> BuiltThisWeekStatisticRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BuiltThisWeekStatisticRepository = Substitute.For<IQueryRepository<BuiltThisWeekStatistic>>();
            this.Sut = new BuiltThisWeekReportService(this.BuiltThisWeekStatisticRepository);
        }
    }
}
