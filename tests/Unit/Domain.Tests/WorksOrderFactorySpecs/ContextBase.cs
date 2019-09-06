namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected WorksOrderFactory Sut { get; private set; }

        protected IWorksOrderProxyService WorksOrderService { get; private set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected IRepository<Part, int> PartsRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderService = Substitute.For<IWorksOrderProxyService>();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.PartsRepository = Substitute.For<IRepository<Part, int>>();
            this.SernosPack = Substitute.For<ISernosPack>();

            this.Sut = new WorksOrderFactory(
                this.WorksOrderService,
                this.WorksOrderRepository,
                this.PartsRepository,
                this.SernosPack);
        }
    }
}
