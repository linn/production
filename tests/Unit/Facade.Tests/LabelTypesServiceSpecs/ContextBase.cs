namespace Linn.Production.Facade.Tests.LabelTypesServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LabelTypeService Sut { get; set; }

        protected IRepository<LabelType, string> LabelTypeRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LabelTypeRepository = Substitute.For<IRepository<LabelType, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new LabelTypeService(this.LabelTypeRepository, this.TransactionManager);
        }
    }
}