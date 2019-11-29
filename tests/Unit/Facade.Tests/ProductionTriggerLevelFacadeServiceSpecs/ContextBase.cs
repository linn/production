namespace Linn.Production.Facade.Tests.ProductionTriggerLevelFacadeServiceSpecs
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> Sut { get; private set; }

        protected IRepository<ProductionTriggerLevel, string> TriggerLevelRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TriggerLevelRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.Sut = new ProductionTriggerLevelService(this.TriggerLevelRepository, this.TransactionManager);
        }
    }
}
