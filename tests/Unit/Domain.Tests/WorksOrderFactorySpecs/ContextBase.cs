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

        protected IRepository<Part, string> PartsRepository { get; private set; }

        protected IRepository<WorkStation, string> WorkStationRepository { get; private set; }

        protected IRepository<ProductionTriggerLevel, string> ProductionTriggerLevelsRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderService = Substitute.For<IWorksOrderProxyService>();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.PartsRepository = Substitute.For<IRepository<Part, string>>();
            this.WorkStationRepository = Substitute.For<IRepository<WorkStation, string>>();
            this.ProductionTriggerLevelsRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.SernosPack = Substitute.For<ISernosPack>();

            this.Sut = new WorksOrderFactory(
                this.WorksOrderService,
                this.WorksOrderRepository,
                this.PartsRepository,
                this.WorkStationRepository,
                this.ProductionTriggerLevelsRepository,
                this.SernosPack);
        }
    }
}
