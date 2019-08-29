namespace Linn.Production.Facade.Tests.ProductionTriggersFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.Services;
    using Linn.Production.Persistence.LinnApps.Repositories;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProductionTriggersFacadeService Sut { get; set; }

        protected IQueryRepository<ProductionTrigger> ProductionTriggerQueryRepository { get; private set; }

        protected IMasterRepository<PtlMaster> PtlMasterRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionTriggerQueryRepository = Substitute.For<IQueryRepository<ProductionTrigger>>();
            this.PtlMasterRepository = Substitute.For<IMasterRepository<PtlMaster>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>> ();
            this.Sut = new ProductionTriggersFacadeService(this.ProductionTriggerQueryRepository, this.CitRepository, this.PtlMasterRepository);
        }
    }
}
