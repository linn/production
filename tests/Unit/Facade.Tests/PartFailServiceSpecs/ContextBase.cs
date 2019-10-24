namespace Linn.Production.Facade.Tests.PartFailServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PartFailFacadeService Sut { get; set; }

        protected IRepository<PartFailErrorType, string> ErrorTypeRepository { get; private set; }

        protected IRepository<PartFail, int> PartFailRepository { get; private set; }
        
        protected IRepository<PartFailFaultCode, string> FaultCodeRepository { get; private set; }

        protected IRepository<StorageLocation, int> StorageLocationRepository { get; private set; }  
        
        protected IRepository<Employee, int> EmployeeRepository { get; private set; }
        
        protected IPartFailService PartFailService { get; private set; }

        protected IDatabaseService DbService { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();

            this.FaultCodeRepository = Substitute.For<IRepository<PartFailFaultCode, string>>();

            this.ErrorTypeRepository = Substitute.For<IRepository<PartFailErrorType, string>>();

            this.StorageLocationRepository = Substitute.For<IRepository<StorageLocation, int>>();

            this.PartFailService = Substitute.For<IPartFailService>();

            this.PartFailRepository = Substitute.For<IRepository<PartFail, int>>();

            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.DbService = Substitute.For<IDatabaseService>();
            
            this.Sut = new PartFailFacadeService(
                    this.DbService,
                    this.PartFailService,
                    this.PartFailRepository,
                    this.ErrorTypeRepository,
                    this.StorageLocationRepository,
                    this.EmployeeRepository,
                    this.FaultCodeRepository,
                    this.TransactionManager);
        }
    }
}