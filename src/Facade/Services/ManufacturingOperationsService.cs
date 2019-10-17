namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingOperationsService : FacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource>
    {
        private readonly IRepository<ManufacturingOperation, int> manufacturingOperationRepository;

        public ManufacturingOperationsService(
            IRepository<ManufacturingOperation, int> repository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.manufacturingOperationRepository = repository;
        }

        protected override ManufacturingOperation CreateFromResource(ManufacturingOperationResource resource)
        {
            return new ManufacturingOperation(
                resource.RouteCode,
                resource.ManufacturingId,
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
    }
}

