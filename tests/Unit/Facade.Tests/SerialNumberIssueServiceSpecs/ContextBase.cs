using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.SerialNumberIssue;
using Linn.Production.Facade.Services;
using NSubstitute;
using NUnit.Framework;

namespace Linn.Production.Facade.Tests.SerialNumberIssueServiceSpecs
{
    public abstract class ContextBase
    {
        protected SerialNumberIssueService Sut { get; set; }

        protected IRepository<SerialNumberIssue, int> SerialNumberIssueRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SerialNumberIssueRepository = Substitute.For<IRepository<SerialNumberIssue, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new SerialNumberIssueService(this.SerialNumberIssueRepository, this.TransactionManager);
        }
    }
}
