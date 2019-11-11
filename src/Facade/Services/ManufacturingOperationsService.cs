namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class ManufacturingOperationsService : FacadeService<ManufacturingOperation, int,
        ManufacturingOperationResource, ManufacturingOperationResource>, IManufacturingOperationsService
    {
        private readonly IDatabaseService databaseService;

        private readonly IRepository<ManufacturingOperation, int> manufacturingOperationsRepository;
        public ManufacturingOperationsService(
            IRepository<ManufacturingOperation, int> repository,
            ITransactionManager transactionManager,
            IDatabaseService databaseService)
            : base(repository, transactionManager)
        {
            this.manufacturingOperationsRepository = repository;
            this.databaseService = databaseService;
        }

        protected override ManufacturingOperation CreateFromResource(ManufacturingOperationResource resource)
        {
            return new ManufacturingOperation(
                resource.RouteCode,
                this.databaseService.GetIdSequence("AN_ARBITRARY_SEQUENCE"),
                resource.OperationNumber,
                resource.Description,
                resource.SkillCode,
                resource.ResourceCode,
                resource.SetAndCleanTime,
                resource.CycleTime,
                resource.LabourPercentage,
                resource.CITCode);
        }

        protected override void UpdateFromResource(ManufacturingOperation entity, ManufacturingOperationResource updateResource)
        {
            entity.RouteCode = updateResource.RouteCode;
            entity.ManufacturingId = updateResource.ManufacturingId;
            entity.OperationNumber = updateResource.OperationNumber;
            entity.Description = updateResource.Description;
            entity.SkillCode = updateResource.SkillCode;
            entity.ResourceCode = updateResource.ResourceCode;
            entity.SetAndCleanTime = updateResource.SetAndCleanTime;
            entity.CycleTime = updateResource.CycleTime;
            entity.LabourPercentage = updateResource.LabourPercentage;
            entity.CITCode = updateResource.CITCode;
        }

        protected override Expression<Func<ManufacturingOperation, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void RemoveOperation(ManufacturingOperation entity)
        {
            this.manufacturingOperationsRepository.Remove(entity);
        }
    }
}

