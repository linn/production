namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class PartFailService : FacadeService<PartFail, int, PartFailResource, PartFailResource>
    {
        private readonly IDatabaseService databaseService;

        private readonly IRepository<PartFail, int> repository;

        private readonly IRepository<PartFailErrorType, string> errorTypeRepository;

        private readonly IRepository<PartFailFaultCode, string> faultCodeRepository;

        private readonly IRepository<WorksOrder, int> worksOrderRepository;

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
            IRepository<PartFailFaultCode, string> faultCodeRepository,
            IRepository<WorksOrder, int> worksOrderRepository, 
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.databaseService = databaseService;
            this.errorTypeRepository = errorTypeRepository;
            this.partRepository = partRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.employeeRepository = employeeRepository;
            this.faultCodeRepository = faultCodeRepository;
            this.worksOrderRepository = worksOrderRepository;
        }

        protected override PartFail CreateFromResource(PartFailResource resource)
        {
            return new PartFail
                       {
                           Id = this.databaseService.GetIdSequence("PART_FAIL_LOG_SEQ"),
                           EnteredBy = this.employeeRepository.FindById(resource.EnteredBy),
                           
                           Batch = resource.Batch,
                           DateCreated = DateTime.Parse(resource.DateCreated),
                           Part = this.partRepository.FindById(resource.PartNumber),
                           Quantity = resource.Quantity,
                           FaultCode = this.faultCodeRepository.FindById(resource.FaultCode),
                           ErrorType = this.errorTypeRepository.FindById(resource.ErrorType),
                           Story = resource.Story,
                           WorksOrder = resource.WorksOrderNumber != null ? this.worksOrderRepository.FindById((int)resource.WorksOrderNumber) : null,
                           StorageLocation = this.storageLocationRepository.FindBy(s => s.LocationCode == resource.StoragePlace),
                           PurchaseOrderNumber = resource.PurchaseOrderNumber,
                           MinutesWasted = resource.MinutesWasted
            };
        }

        protected override void UpdateFromResource(PartFail partFail, PartFailResource resource)
        {
            partFail.Batch = resource.Batch;
            partFail.Part = this.partRepository.FindById(resource.PartNumber);
            partFail.Quantity = resource.Quantity;
            partFail.FaultCode = this.faultCodeRepository.FindById(resource.FaultCode);
            partFail.ErrorType = this.errorTypeRepository.FindById(resource.ErrorType);
            partFail.Story = resource.Story;
            partFail.WorksOrder = resource.WorksOrderNumber 
                                  != null ? this.worksOrderRepository.FindById((int)resource.WorksOrderNumber) : null;
            partFail.StorageLocation =
                this.storageLocationRepository.FindBy(s => s.LocationCode == resource.StoragePlace);
            partFail.PurchaseOrderNumber = resource.PurchaseOrderNumber;
            partFail.MinutesWasted = resource.MinutesWasted;
        }

        protected override Expression<Func<PartFail, bool>> SearchExpression(string searchTerm)
        {
            return fail => fail.Id.ToString().Contains(searchTerm);
        }
    }
}