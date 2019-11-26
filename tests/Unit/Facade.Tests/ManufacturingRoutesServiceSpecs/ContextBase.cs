namespace Linn.Production.Facade.Tests.ManufacturingRoutesServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ManufacturingRouteService Sut { get; set; }

        protected ManufacturingOperationsService ManufacturingOperationsService { get; private set; }

        protected IRepository<ManufacturingOperation, int> ManufacturingOperationRepository { get; private set; }

        protected IRepository<ManufacturingRoute, string> ManufacturingRouteRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IDatabaseService databaseService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ManufacturingRouteRepository = Substitute.For<IRepository<ManufacturingRoute, string>>();
            this.ManufacturingOperationRepository = Substitute.For<IRepository<ManufacturingOperation, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.databaseService = Substitute.For<IDatabaseService>();
            this.ManufacturingOperationsService = new ManufacturingOperationsService(
                this.ManufacturingOperationRepository,
                this.TransactionManager,
                this.databaseService);
            this.Sut = new ManufacturingRouteService(
                this.ManufacturingRouteRepository,
                this.TransactionManager,
                this.ManufacturingOperationsService);
        }
    }
}