namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Persistence.LinnApps.Repositories;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProductionTriggersFacadeService Sut { get; set; }

        protected Domain.LinnApps.Repositories.IQueryRepository<ProductionTrigger> ProductionTriggerQueryRepository { get; private set; }

        protected IMasterRepository<PtlMaster> PtlMasterRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected IRepository<AccountingCompany, string> AccountingCompanyRepository { get; private set; }

        protected IQueryRepository<ProductionBackOrder> productionQueryRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionTriggerQueryRepository = Substitute.For<Domain.LinnApps.Repositories.IQueryRepository<ProductionTrigger>>();
            this.PtlMasterRepository = Substitute.For<IMasterRepository<PtlMaster>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>> ();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();

            this.AccountingCompanyRepository = Substitute.For<IRepository<AccountingCompany, string>>();
            this.Sut = new ProductionTriggersFacadeService(this.ProductionTriggerQueryRepository, this.CitRepository, this.PtlMasterRepository, this.WorksOrderRepository, this.productionQueryRepository, this.AccountingCompanyRepository);
        }
    }
}
