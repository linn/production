namespace Linn.Production.Facade.Tests.AssemblyFailsServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected AssemblyFailsService Sut { get; set; }

        protected IRepository<AssemblyFail, int> AssemblyFailRepository { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        protected IRepository<Part, string> PartRepository { get; private set; }

        protected IRepository<AssemblyFailFaultCode, string> FaultCodeRepository { get; private set; }

        protected IAssemblyFailsDomainService domainService;

        protected ITransactionManager TransactionManager { get; private set; }

        protected IDatabaseService DbService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AssemblyFailRepository = Substitute.For<IRepository<AssemblyFail, int>>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>>();
            this.PartRepository = Substitute.For<IRepository<Part, string>>();
            this.FaultCodeRepository = Substitute.For<IRepository<AssemblyFailFaultCode, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.DbService = Substitute.For<IDatabaseService>();
            this.domainService = Substitute.For<IAssemblyFailsDomainService>();
            this.Sut = new AssemblyFailsService(
                this.AssemblyFailRepository,
                this.EmployeeRepository,
                this.FaultCodeRepository,
                this.WorksOrderRepository,
                this.CitRepository,
                this.PartRepository,
                this.TransactionManager,
                this.DbService,
                this.domainService);
        }
    }
}