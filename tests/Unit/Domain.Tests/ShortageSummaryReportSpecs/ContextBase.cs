namespace Linn.Production.Domain.Tests.ShortageSummaryReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ShortageSummaryReportService Sut { get; set; }

        protected IRepository<AccountingCompany, string> AccountingCompanyRepository { get; private set; }

        protected ISingleRecordRepository<PtlMaster> PtlMasterRepository { get; private set; }

        protected IQueryRepository<ProductionTrigger> ProductionTriggerRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        protected IQueryRepository<ProductionBackOrder> ProductionBackOrderRepository { get; private set; }

        protected IQueryRepository<WswShortage> ShortageRepository { get; private set; }

        protected IQueryRepository<WswShortageStory> WswShortageStoryRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AccountingCompanyRepository = Substitute.For<IRepository<AccountingCompany, string>>();
            this.PtlMasterRepository = Substitute.For<ISingleRecordRepository<PtlMaster>>();
            this.ProductionTriggerRepository = Substitute.For<IQueryRepository<ProductionTrigger>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>>();
            this.ProductionBackOrderRepository = Substitute.For<IQueryRepository<ProductionBackOrder>>();
            this.ShortageRepository = Substitute.For<IQueryRepository<WswShortage>>();
            this.WswShortageStoryRepository = Substitute.For<IQueryRepository<WswShortageStory>>();
            this.Sut = new ShortageSummaryReportService(
                this.AccountingCompanyRepository,
                this.PtlMasterRepository,
                this.ProductionTriggerRepository,
                this.CitRepository,
                this.ProductionBackOrderRepository,
                this.ShortageRepository,
                this.WswShortageStoryRepository);
        }
    }
}
