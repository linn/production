namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WorksOrderService Sut { get; set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IFacadeService<Part, string, PartResource, PartResource> PartsService { get; private set; }

        protected IGetNextBatchService GetNextBatchService { get; private set; }

        protected ICanRaiseWorksOrderService CanRaiseWorksOrderService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PartsService = Substitute.For<IFacadeService<Part, string, PartResource, PartResource>>();
            this.GetNextBatchService = Substitute.For<IGetNextBatchService>();
            this.CanRaiseWorksOrderService = Substitute.For<ICanRaiseWorksOrderService>();

            this.Sut = new WorksOrderService(
                this.WorksOrderRepository,
                this.TransactionManager,
                this.PartsService,
                this.GetNextBatchService,
                this.CanRaiseWorksOrderService);
        }
    }
}
