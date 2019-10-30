namespace Linn.Production.Facade.Tests.BoardFailTypesServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BoardFailTypesService Sut { get; set; }

        protected IRepository<BoardFailType, int> BoardFailTypeRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BoardFailTypeRepository = Substitute.For<IRepository<BoardFailType, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new BoardFailTypesService(this.BoardFailTypeRepository, this.TransactionManager);
        }
    }
}