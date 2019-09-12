namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WorksOrdersService Sut { get; set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IWorksOrderFactory WorksOrderFactory { get; private set; }

        protected IWorksOrderProxyService WorksOrderProxyService { get; private set; }

        protected IProductAuditPack ProductAuditPack { get; private set;  }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.WorksOrderFactory = Substitute.For<IWorksOrderFactory>();
            this.WorksOrderProxyService = Substitute.For<IWorksOrderProxyService>();
            this.ProductAuditPack = Substitute.For<IProductAuditPack>();

            this.Sut = new WorksOrdersService(
                this.WorksOrderRepository,
                this.TransactionManager,
                this.WorksOrderFactory,
                this.ProductAuditPack);
        }
    }
}
