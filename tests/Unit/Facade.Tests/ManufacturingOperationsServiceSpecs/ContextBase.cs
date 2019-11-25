namespace Linn.Production.Facade.Tests.ManufacturingOperationsServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ManufacturingOperationsService Sut { get; set; }

        protected IRepository<ManufacturingOperation, int> ManufacturingOperationRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IDatabaseService databaseService { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ManufacturingOperationRepository = Substitute.For<IRepository<ManufacturingOperation, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.databaseService = Substitute.For<IDatabaseService>();
            this.Sut = new ManufacturingOperationsService(this.ManufacturingOperationRepository, this.TransactionManager, this.databaseService);
        }
    }
}