namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class PartFailFacadeService : FacadeService<PartFail, int, PartFailResource, PartFailResource>
    {
        private readonly IDatabaseService databaseService;

        private readonly IRepository<PartFailErrorType, string> errorTypeRepository;

        private readonly IRepository<PartFailFaultCode, string> faultCodeRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IPartFailService partFailService;

        public PartFailFacadeService(
            IDatabaseService databaseService,
            IPartFailService partFailService,
            IRepository<PartFail, int> repository,
            IRepository<PartFailErrorType, string> errorTypeRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IRepository<Employee, int> employeeRepository,
            IRepository<PartFailFaultCode, string> faultCodeRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.databaseService = databaseService;
            this.errorTypeRepository = errorTypeRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.employeeRepository = employeeRepository;
            this.faultCodeRepository = faultCodeRepository;
            this.partFailService = partFailService;
        }

        protected override PartFail CreateFromResource(PartFailResource resource)
        {
            var candidate = new PartFail
                                {
                                    Id = this.databaseService.GetNextVal("PART_FAIL_LOG_SEQ"),
                                    EnteredBy = this.employeeRepository.FindById(resource.EnteredBy),
                                    Batch = resource.Batch,
                                    DateCreated = DateTime.Parse(resource.DateCreated),
                                    Part = new Part { PartNumber = resource.PartNumber },
                                    Quantity = resource.Quantity,
                                    FaultCode = this.faultCodeRepository.FindById(resource.FaultCode),
                                    ErrorType = this.errorTypeRepository.FindById(resource.ErrorType),
                                    Story = resource.Story,
                                    WorksOrder =
                                        resource.WorksOrderNumber != null
                                            ? new WorksOrder { OrderNumber = (int)resource.WorksOrderNumber }
                                            : null,
                                    StorageLocation =
                                        this.storageLocationRepository.FindBy(
                                            s => s.LocationCode == resource.StoragePlace),
                                    PurchaseOrderNumber = resource.PurchaseOrderNumber,
                                    MinutesWasted = resource.MinutesWasted,
                                    SerialNumber = resource.SerialNumber,
                                    Comments = resource.Comments,
                                    Owner = resource.Owner != null
                                                ? this.employeeRepository.FindById((int)resource.Owner)
                                                : null,
                                    NoCost = resource.NoCost ? "Y" : "N"
            };

            return this.partFailService.Create(candidate);
        }

        protected override void UpdateFromResource(PartFail partFail, PartFailResource resource)
        {
            var candidate = new PartFail
                                {
                                    Id = partFail.Id,
                                    EnteredBy = partFail.EnteredBy,
                                    Batch = resource.Batch,
                                    DateCreated = DateTime.Parse(resource.DateCreated),
                                    Part = new Part { PartNumber = resource.PartNumber },
                                    Quantity = resource.Quantity,
                                    FaultCode = this.faultCodeRepository.FindById(resource.FaultCode),
                                    ErrorType = this.errorTypeRepository.FindById(resource.ErrorType),
                                    Story = resource.Story,
                                    SerialNumber = resource.SerialNumber,
                                    WorksOrder = resource.WorksOrderNumber != null
                                                     ? new WorksOrder { OrderNumber = (int)resource.WorksOrderNumber }
                                                     : null,
                                    StorageLocation = this.storageLocationRepository
                                        .FindBy(s => s.LocationCode == resource.StoragePlace),
                                    PurchaseOrderNumber = resource.PurchaseOrderNumber,
                                    MinutesWasted = resource.MinutesWasted,
                                    Comments = resource.Comments,
                                    Owner = resource.Owner != null ? this.employeeRepository.FindById((int)resource.Owner) : null,
                                    NoCost = resource.NoCost ? "Y" : "N",
            };

            partFail.UpdateFrom(this.partFailService.Create(candidate));
        }

        protected override Expression<Func<PartFail, bool>> SearchExpression(string searchTerm)
        {
            return fail => fail.Id.ToString().Contains(searchTerm);
        }
    }
}