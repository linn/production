namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProductionTriggersFacadeService Sut { get; set; }

        protected IQueryRepository<ProductionTrigger> ProductionTriggerQueryRepository { get; private set; }

        protected ISingleRecordRepository<PtlMaster> PtlMasterRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected IRepository<AccountingCompany, string> AccountingCompanyRepository { get; private set; }

        protected IQueryRepository<ProductionBackOrder> ProductionBackOrderQueryRepository { get; private set; }

        protected IQueryRepository<ProductionTriggerAssembly> ProductionTriggerAssemblyQueryRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionTriggerQueryRepository = Substitute.For<IQueryRepository<ProductionTrigger>>();
            this.PtlMasterRepository = Substitute.For<ISingleRecordRepository<PtlMaster>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>>();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.ProductionBackOrderQueryRepository = Substitute.For<IQueryRepository<ProductionBackOrder>>();
            this.ProductionTriggerAssemblyQueryRepository =
                Substitute.For<IQueryRepository<ProductionTriggerAssembly>>();

            this.AccountingCompanyRepository = Substitute.For<IRepository<AccountingCompany, string>>();
            this.Sut = new ProductionTriggersFacadeService(
                this.ProductionTriggerQueryRepository,
                this.CitRepository,
                this.PtlMasterRepository,
                this.WorksOrderRepository,
                this.ProductionBackOrderQueryRepository,
                this.AccountingCompanyRepository,
                this.ProductionTriggerAssemblyQueryRepository);
        }
    }
}
