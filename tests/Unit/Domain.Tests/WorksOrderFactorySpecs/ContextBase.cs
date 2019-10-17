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

        protected IRepository<Part, string> PartsRepository { get; private set; }

        protected IRepository<ProductionTriggerLevel, string> ProductionTriggerLevelsRepository { get; private set; }

        protected IWorksOrderUtilities WorksOrderUtilities { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderService = Substitute.For<IWorksOrderProxyService>();
            this.PartsRepository = Substitute.For<IRepository<Part, string>>();
            this.ProductionTriggerLevelsRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.WorksOrderUtilities = Substitute.For<IWorksOrderUtilities>();

            this.Sut = new WorksOrderFactory(
                this.WorksOrderService,
                this.PartsRepository,
                this.ProductionTriggerLevelsRepository,
                this.WorksOrderUtilities);
        }
    }
}
