namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class PartFailService : FacadeService<PartFail, int, PartFailResource, PartFailResource>
    {
        private readonly IDatabaseService databaseService;

        private readonly IRepository<PartFail, int> repository;

        private readonly IRepository<PartFailErrorType, string> errorTypeRepository;

        private readonly IRepository<Part, string> partRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IRepository<Employee, int> employeeRepository;

        public PartFailService(
            IDatabaseService databaseService,
            IRepository<PartFail, int> repository,
            IRepository<PartFailErrorType, string> errorTypeRepository,
            IRepository<Part, string> partRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IRepository<Employee, int> employeeRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.databaseService = databaseService;
            this.errorTypeRepository = errorTypeRepository;
            this.partRepository = partRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.employeeRepository = employeeRepository;
        }

        protected override PartFail CreateFromResource(PartFailResource resource)
        {
            return new PartFail
                       {
                           Id = this.databaseService.GetIdSequence("PART_FAIL_LOG_SEQ"),
                           EnteredBy = this.employeeRepository.FindById(resource.EnteredBy)
                       };
        }

        protected override void UpdateFromResource(PartFail entity, PartFailResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartFail, bool>> SearchExpression(string searchTerm)
        {
            return fail => fail.Id.ToString().Contains(searchTerm);
        }
    }
}