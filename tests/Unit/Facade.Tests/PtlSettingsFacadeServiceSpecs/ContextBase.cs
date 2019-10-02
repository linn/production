namespace Linn.Production.Facade.Tests.PtlSettingsFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.Services;
    using Linn.Production.Persistence.LinnApps.Repositories;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PtlSettingsFacadeService Sut { get; private set; }

        protected IMasterRepository<PtlSettings> PtlSettingsRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }
        [SetUp]
        public void SetUpContext()
        {
            this.PtlSettingsRepository = Substitute.For<IMasterRepository<PtlSettings>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.Sut = new PtlSettingsFacadeService(this.PtlSettingsRepository, this.TransactionManager);
        }
    }
}
