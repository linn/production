namespace Linn.Production.Facade.Tests.AteTestServiceSpecs
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AteTestService Sut { get; private set; }

        protected IRepository<AteTest, int> AteTestRepository { get; private set; }

        protected IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> DetailService { get; private set; }

        private IRepository<WorksOrder, int> WorksOrderRepository { get; set; }

        private IRepository<Employee, int> EmployeeRepository { get; set; }

        private ITransactionManager TransactionManager { get; set; }

        private IDatabaseService DatabaseService { get; set; }

        private IRepository<PcasRevision, string> PcasRevisionRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.AteTestRepository = Substitute.For<IRepository<AteTest, int>>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.PcasRevisionRepository = Substitute.For<IRepository<PcasRevision, string>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.DetailService = Substitute
                .For<IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource>>();
            this.Sut = new AteTestService(
                this.AteTestRepository,
                this.TransactionManager,
                this.WorksOrderRepository,
                this.EmployeeRepository,
                this.PcasRevisionRepository,
                this.DatabaseService,
                this.DetailService);
        }
    }
}