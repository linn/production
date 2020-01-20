namespace Linn.Production.Facade.Tests.AteTestDetailSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected AteTestDetailService Sut { get; private set; }

        protected IRepository<AteTestDetail, AteTestDetailKey> AteTestDetailRepository { get; private set; }

        protected IRepository<AteTest, int> AteTestRepository { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        private ITransactionManager TransactionManager { get; set; }



        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.AteTestDetailRepository = Substitute.For<IRepository<AteTestDetail, AteTestDetailKey>>();
            this.AteTestRepository = Substitute.For<IRepository<AteTest, int>>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.Sut = new AteTestDetailService(
                this.AteTestDetailRepository, 
                this.EmployeeRepository, 
                this.AteTestRepository, 
                this.TransactionManager);
        }
    }
}