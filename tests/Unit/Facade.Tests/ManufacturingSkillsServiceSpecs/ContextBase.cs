namespace Linn.Production.Facade.Tests.ManufacturingSkillsServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ManufacturingSkillService Sut { get; set; }

        protected IRepository<ManufacturingSkill, string> ManufacturingSkillRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ManufacturingSkillRepository = Substitute.For<IRepository<ManufacturingSkill, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ManufacturingSkillService(this.ManufacturingSkillRepository, this.TransactionManager);
        }
    }
}