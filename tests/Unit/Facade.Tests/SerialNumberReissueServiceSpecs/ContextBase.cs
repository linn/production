namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SerialNumberReissueService Sut { get; set; }

        protected IRepository<SerialNumberReissue, int> SerialNumberReissueRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SerialNumberReissueRepository = Substitute.For<IRepository<SerialNumberReissue, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new SerialNumberReissueService(this.SerialNumberReissueRepository, this.TransactionManager);
        }
    }
}
