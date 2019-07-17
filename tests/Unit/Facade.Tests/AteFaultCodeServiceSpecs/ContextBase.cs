namespace Linn.Production.Facade.Tests.AteFaultCodeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AteFaultCodeService Sut { get; set; }

        protected IRepository<AteFaultCode, string> AteFaultCodeRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AteFaultCodeRepository = Substitute.For<IRepository<AteFaultCode, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new AteFaultCodeService(this.AteFaultCodeRepository, this.TransactionManager);
        }
    }
}